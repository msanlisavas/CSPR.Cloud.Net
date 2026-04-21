using System;
using System.Net.WebSockets;

namespace CSPR.Cloud.Net.Objects.Socket
{
    /// <summary>
    /// Controls automatic reconnection for streaming subscriptions.
    /// <para>
    /// Disabled by default. When <see cref="Enabled"/> is true, <c>SubscribeAsync</c> will transparently
    /// reconnect on transport failures and server-side disconnects using exponential backoff with jitter.
    /// Combine with <c>persistentSessionId</c> on <see cref="Clients.CasperCloudSocketClient"/> to replay
    /// messages queued while the client was offline.
    /// </para>
    /// <para>
    /// Per the CSPR Cloud docs, "WebSocket connections may close during API deployments, necessitating
    /// reconnection logic in client applications" — enabling this policy is the built-in answer to that.
    /// </para>
    /// </summary>
    public class StreamReconnectPolicy
    {
        /// <summary>
        /// Turns reconnect on. When false, <c>SubscribeAsync</c> uses its legacy single-attempt behavior:
        /// returns on clean server close, throws on transport failure.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>Delay before the first reconnect attempt. Default: 1 second.</summary>
        public TimeSpan InitialDelay { get; set; } = TimeSpan.FromSeconds(1);

        /// <summary>Upper bound on the per-attempt delay. Default: 60 seconds.</summary>
        public TimeSpan MaxDelay { get; set; } = TimeSpan.FromSeconds(60);

        /// <summary>Multiplier applied to the delay after each failure. Default: 2.0.</summary>
        public double BackoffMultiplier { get; set; } = 2.0;

        /// <summary>
        /// Fraction of the computed delay to perturb randomly, to avoid thundering-herd effects
        /// after a shared outage. 0 disables jitter. Default: 0.25 (±25%).
        /// </summary>
        public double JitterFactor { get; set; } = 0.25;

        /// <summary>
        /// Maximum number of reconnect attempts before giving up. <c>-1</c> means retry forever.
        /// Default: -1.
        /// </summary>
        public int MaxRetries { get; set; } = -1;

        /// <summary>
        /// Optional gate that can veto a reconnect based on the triggering exception and attempt
        /// count. Return <c>true</c> to reconnect, <c>false</c> to let the original exception bubble.
        /// When null, <see cref="DefaultShouldReconnect"/> is used.
        /// </summary>
        public Func<Exception, int, bool> ShouldReconnect { get; set; }

        /// <summary>
        /// Default reconnect gate: retries transport / transient errors (including clean server
        /// disconnects, surfaced as <c>null</c>), declines retry on argument errors.
        /// </summary>
        public static bool DefaultShouldReconnect(Exception ex, int attempt)
        {
            // Clean server close is represented by a null exception — always worth reconnecting.
            if (ex == null) return true;

            // Programmer errors — no point retrying.
            if (ex is ArgumentException) return false;
            if (ex is InvalidOperationException) return false;

            // Anything transport-shaped — transient.
            if (ex is WebSocketException) return true;
            if (ex is System.IO.IOException) return true;
            if (ex is System.Net.Http.HttpRequestException) return true;
            if (ex is TimeoutException) return true;

            // Unknown — assume transient so we recover automatically rather than silently dying.
            return true;
        }

        /// <summary>
        /// Computes the delay for attempt <paramref name="attempt"/> (1-based) given the policy.
        /// Exposed for unit tests; production callers don't need to call this directly.
        /// </summary>
        public TimeSpan ComputeDelay(int attempt, Random random = null)
        {
            if (attempt < 1) attempt = 1;

            double ms = InitialDelay.TotalMilliseconds * Math.Pow(BackoffMultiplier <= 0 ? 1.0 : BackoffMultiplier, attempt - 1);
            ms = Math.Min(ms, MaxDelay.TotalMilliseconds);
            if (ms < 0) ms = 0;

            if (JitterFactor > 0)
            {
                var rng = random ?? new Random();
                // Symmetric jitter: ms ∈ [ms * (1 - j), ms * (1 + j)]
                double span = ms * JitterFactor;
                ms = ms - span + (rng.NextDouble() * 2 * span);
                ms = Math.Max(0, Math.Min(ms, MaxDelay.TotalMilliseconds));
            }

            return TimeSpan.FromMilliseconds(ms);
        }
    }
}
