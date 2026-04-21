using CSPR.Cloud.Net.Errors;
using CSPR.Cloud.Net.Objects.Socket;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSPR.Cloud.Net.Clients.Socket
{
    /// <summary>
    /// Shared low-level WebSocket subscription loop used by all streaming endpoints. Handles
    /// connection, message reassembly across frames, JSON deserialization into
    /// <see cref="WebSocketMessage{T}"/>, and dispatch to the caller-supplied handler. Optional
    /// reconnect-with-backoff is layered on top via <see cref="StreamReconnectPolicy"/>.
    /// </summary>
    internal static class SocketStreamHandler
    {
        /// <summary>
        /// Opens a subscription against <paramref name="uri"/> and pumps every message into
        /// <paramref name="onMessage"/> until cancellation. Without a reconnect policy it stops on
        /// the first clean server close or transport failure; with <see cref="StreamReconnectPolicy.Enabled"/>
        /// it transparently reconnects using exponential backoff + jitter.
        /// </summary>
        public static Task SubscribeAsync<T>(
            Uri uri,
            string apiKey,
            string persistentSessionId,
            Func<WebSocketMessage<T>, Task> onMessage,
            Func<Exception, Task> onError,
            Func<ClientWebSocket> webSocketFactory,
            StreamReconnectPolicy reconnectPolicy,
            Func<Exception, int, Task> onReconnecting,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            if (string.IsNullOrEmpty(apiKey)) throw new ArgumentException("API key is required.", nameof(apiKey));
            if (onMessage == null) throw new ArgumentNullException(nameof(onMessage));

            return RunWithReconnectAsync(
                pumpOnce: ct => ConnectAndPumpOnceAsync(
                    uri, apiKey, persistentSessionId,
                    onMessage, onError, webSocketFactory, logger, ct),
                reconnectPolicy: reconnectPolicy,
                onReconnecting: onReconnecting,
                logger: logger,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Runs <paramref name="pumpOnce"/> until cancellation, applying <paramref name="reconnectPolicy"/>
        /// between attempts. Exposed as <c>internal</c> so the reconnect semantics can be exercised in
        /// tests without opening real sockets.
        /// </summary>
        internal static async Task RunWithReconnectAsync(
            Func<CancellationToken, Task> pumpOnce,
            StreamReconnectPolicy reconnectPolicy,
            Func<Exception, int, Task> onReconnecting,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            if (pumpOnce == null) throw new ArgumentNullException(nameof(pumpOnce));

            bool reconnectEnabled = reconnectPolicy != null && reconnectPolicy.Enabled;
            int attempt = 0;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Exception pumpException = null;
                try
                {
                    await pumpOnce(cancellationToken).ConfigureAwait(false);

                    // Normal return == server sent a clean close or receive loop exited without error.
                    // Treat as a reconnect candidate when the policy is on; otherwise we're done.
                    if (!reconnectEnabled) return;
                }
                catch (OperationCanceledException)
                {
                    // User cancelled — always exit regardless of policy.
                    throw;
                }
                catch (Exception ex)
                {
                    pumpException = ex;
                    if (!reconnectEnabled) throw;
                }

                // At this point the pump ended without cancellation and reconnect is enabled.
                var gate = reconnectPolicy.ShouldReconnect ?? StreamReconnectPolicy.DefaultShouldReconnect;
                attempt++;
                bool overBudget = reconnectPolicy.MaxRetries >= 0 && attempt > reconnectPolicy.MaxRetries;

                if (overBudget || !gate(pumpException, attempt))
                {
                    if (pumpException != null) throw pumpException;
                    return;
                }

                if (onReconnecting != null)
                {
                    try { await onReconnecting(pumpException, attempt).ConfigureAwait(false); }
                    catch (Exception cbEx) { logger?.LogWarning(cbEx, "onReconnecting callback threw"); }
                }

                var delay = reconnectPolicy.ComputeDelay(attempt);
                logger?.LogInformation("Reconnecting in {Delay} (attempt {Attempt})", delay, attempt);

                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// One connect + receive session. Returns normally if the server sends a clean Close or if
        /// cancellation fires mid-pump. Throws on transport failures and on parse/handler errors
        /// that <paramref name="onError"/> chose to re-raise.
        /// </summary>
        private static async Task ConnectAndPumpOnceAsync<T>(
            Uri uri,
            string apiKey,
            string persistentSessionId,
            Func<WebSocketMessage<T>, Task> onMessage,
            Func<Exception, Task> onError,
            Func<ClientWebSocket> webSocketFactory,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            using (var ws = webSocketFactory != null ? webSocketFactory() : new ClientWebSocket())
            {
                ws.Options.SetRequestHeader("Authorization", apiKey);
                if (!string.IsNullOrEmpty(persistentSessionId))
                {
                    ws.Options.SetRequestHeader("Persistent-Session", persistentSessionId);
                }

                try
                {
                    await ws.ConnectAsync(uri, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (WebSocketException ex)
                {
                    // Preserve legacy public behavior: raise UnauthorizedException on upgrade
                    // failures. The reconnect policy's default gate treats WebSocketException as
                    // transient, so we attach the original as the inner for correct retry logic.
                    logger?.LogError(ex, "Failed to open WebSocket to {Uri}", uri);
                    throw new UnauthorizedException($"WebSocket connection failed: {ex.Message}", logger);
                }

                var buffer = new byte[16 * 1024];
                var segment = new ArraySegment<byte>(buffer);

                while (ws.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
                {
                    using (var payload = new MemoryStream())
                    {
                        WebSocketReceiveResult result;

                        // Receive frames. Transport failures propagate — the outer reconnect loop
                        // is the right place to decide whether to retry.
                        do
                        {
                            result = await ws.ReceiveAsync(segment, cancellationToken).ConfigureAwait(false);
                            if (result.MessageType == WebSocketMessageType.Close)
                            {
                                if (ws.State == WebSocketState.CloseReceived)
                                {
                                    await ws.CloseAsync(
                                        WebSocketCloseStatus.NormalClosure,
                                        "client acknowledging close",
                                        CancellationToken.None).ConfigureAwait(false);
                                }
                                return;
                            }
                            payload.Write(buffer, 0, result.Count);
                        }
                        while (!result.EndOfMessage && !cancellationToken.IsCancellationRequested);

                        if (cancellationToken.IsCancellationRequested) return;
                        if (payload.Length == 0) continue;

                        var json = Encoding.UTF8.GetString(payload.ToArray());

                        // The server sends plaintext keepalives (e.g. "Ping") as text frames
                        // between JSON envelopes. Skip anything that isn't a JSON object/array
                        // so the pump keeps running instead of throwing a parse error.
                        if (!IsJsonEnvelope(json))
                        {
                            logger?.LogTrace("Skipping non-JSON stream frame: {Payload}", json);
                            continue;
                        }

                        WebSocketMessage<T> envelope;
                        try
                        {
                            envelope = JsonConvert.DeserializeObject<WebSocketMessage<T>>(json);
                        }
                        catch (Exception ex)
                        {
                            // Deserialization is treated as a per-message problem, not a transport
                            // problem — route to onError and keep the socket alive.
                            logger?.LogWarning(ex, "Failed to parse streaming message: {Json}", json);
                            if (onError != null)
                            {
                                await onError(ex).ConfigureAwait(false);
                                continue;
                            }
                            throw;
                        }

                        if (envelope != null)
                        {
                            await onMessage(envelope).ConfigureAwait(false);
                        }
                    }
                }
            }
        }

        internal static bool IsJsonEnvelope(string payload)
        {
            for (int i = 0; i < payload.Length; i++)
            {
                char c = payload[i];
                if (c == ' ' || c == '\t' || c == '\r' || c == '\n') continue;
                return c == '{' || c == '[';
            }
            return false;
        }
    }
}
