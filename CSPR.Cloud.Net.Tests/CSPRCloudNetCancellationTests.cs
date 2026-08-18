using CSPR.Cloud.Net.Clients;
using CSPR.Cloud.Net.Objects.Abstract;
using CSPR.Cloud.Net.Objects.Config;
using CSPR.Cloud.Net.Objects.Transfer;
using CSPR.Cloud.Net.Parameters.Wrapper.Transfer;
using System.Net;
using System.Text;
using Xunit;

namespace CSPR.Cloud.Net.Tests
{
    /// <summary>
    /// Covers <see cref="CancellationToken"/> propagation through the REST client (new in v4.1.0).
    /// Offline: the client takes an <see cref="HttpClient"/>, so a stub handler stands in for the
    /// network and no API key is involved. The token has to travel facade → CommonEndpoint →
    /// GetDataAsync/PostDataAsync → HttpClient.SendAsync; these tests pin both ends of that chain,
    /// and the uniform threading in between is what lets one endpoint's chain stand in for all of
    /// them.
    /// </summary>
    public class CSPRCloudNetCancellationTests
    {
        /// <summary>
        /// A server that answers only after 30 seconds — far longer than any of these tests waits,
        /// short enough that a regression fails the test instead of hanging the run. Cancellation is
        /// the only way a call against this handler finishes quickly, so a test that completes at
        /// all has proven the token reached the handler.
        /// </summary>
        private sealed class SlowHandler : HttpMessageHandler
        {
            public int Invocations;

            private readonly TaskCompletionSource<bool> _entered =
                new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            /// <summary>Completes once the request is in flight, so a test can cancel at a
            /// deterministic point instead of racing a timer against request start-up.</summary>
            public Task Entered => _entered.Task;

            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Interlocked.Increment(ref Invocations);
                _entered.TrySetResult(true);
                await Task.Delay(TimeSpan.FromSeconds(30), cancellationToken);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{}", Encoding.UTF8, "application/json"),
                };
            }
        }

        private static CasperCloudRestClient ClientOver(HttpMessageHandler handler) =>
            new CasperCloudRestClient(new CasperCloudClientConfig("test-key"), new HttpClient(handler));

        [Fact]
        public async Task GetDataAsync_PreCanceledToken_ThrowsOperationCanceled()
        {
            var client = ClientOver(new SlowHandler());
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
                client.GetDataAsync<PaginatedResponse<TransferData>>(
                    "https://example.invalid/transfers", cts.Token));
        }

        [Fact]
        public async Task GetDataAsync_TokenCanceledMidRequest_AbortsTheInFlightCall()
        {
            var handler = new SlowHandler();
            var client = ClientOver(handler);
            using var cts = new CancellationTokenSource();

            var call = client.GetDataAsync<PaginatedResponse<TransferData>>(
                "https://example.invalid/transfers", cts.Token);
            await handler.Entered;
            cts.Cancel();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => call);

            // The request was already in flight — cancellation ended it, not the server.
            Assert.Equal(1, handler.Invocations);
        }

        [Fact]
        public async Task PostDataAsync_TokenCanceledMidRequest_AbortsTheInFlightCall()
        {
            var handler = new SlowHandler();
            var client = ClientOver(handler);
            using var cts = new CancellationTokenSource();

            var call = client.PostDataAsync<Response<bool>>(
                "https://example.invalid/deploys", new { }, cts.Token);
            await handler.Entered;
            cts.Cancel();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => call);

            Assert.Equal(1, handler.Invocations);
        }

        [Fact]
        public async Task FacadeEndpoint_CanceledToken_PropagatesThroughTheWholeChain()
        {
            // Compiling at all proves the facade signature accepts a token; finishing quickly
            // proves the token was forwarded rather than dropped on the way down.
            var client = ClientOver(new SlowHandler());
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
                client.Testnet.Transfer.GetAccountTransfersAsync(
                    "account-identifier", new TransferAccountRequestParameters(), cts.Token));
        }
    }
}
