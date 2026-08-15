using CSPR.Cloud.Net.Clients;
using CSPR.Cloud.Net.Objects.Abstract;
using CSPR.Cloud.Net.Objects.Config;
using CSPR.Cloud.Net.Objects.Transfer;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace CSPR.Cloud.Net.Tests
{
    /// <summary>
    /// Covers <see cref="CasperCloudClientConfig.TolerateMalformedRows"/> and the decimal typing of
    /// money values. Offline: the client takes an <see cref="HttpClient"/>, so a stub handler serves
    /// the payload and no API key or network is involved.
    /// </summary>
    public class CSPRCloudNetTolerantRowsTests
    {
        private sealed class StubHandler : HttpMessageHandler
        {
            private readonly string _body;
            public StubHandler(string body) => _body = body;

            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, CancellationToken cancellationToken) =>
                Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(_body, Encoding.UTF8, "application/json")
                });
        }

        private static CasperCloudRestClient ClientFor(string body, bool tolerateMalformedRows) =>
            new CasperCloudRestClient(
                new CasperCloudClientConfig("test-key") { TolerateMalformedRows = tolerateMalformedRows },
                new HttpClient(new StubHandler(body)));

        // Middle row's block_height is not a number, so it cannot be deserialized into ulong?.
        private const string PageWithOneBadRow = @"{
            ""data"": [
                { ""deploy_hash"": ""aaaa"", ""block_height"": 900, ""amount"": ""2500000000"", ""rate"": 0.0512 },
                { ""deploy_hash"": ""bbbb"", ""block_height"": ""not-a-number"", ""amount"": ""1"" },
                { ""deploy_hash"": ""cccc"", ""block_height"": 902, ""amount"": ""5"", ""rate"": 0.0512 }
            ],
            ""item_count"": 3,
            ""page_count"": 1
        }";

        private const string CleanPage = @"{
            ""data"": [
                { ""deploy_hash"": ""aaaa"", ""block_height"": 900, ""amount"": ""2500000000"", ""rate"": 0.0512 }
            ],
            ""item_count"": 1,
            ""page_count"": 1
        }";

        [Fact]
        public async Task GetDataAsync_MalformedRowWithToleranceDisabled_ThrowsForWholePage()
        {
            // Unchanged legacy behaviour — opting in is required to get anything else.
            var client = ClientFor(PageWithOneBadRow, tolerateMalformedRows: false);

            await Assert.ThrowsAnyAsync<JsonException>(() =>
                client.GetDataAsync<PaginatedResponse<TransferData>>("https://example.invalid/transfers"));
        }

        [Fact]
        public async Task GetDataAsync_MalformedRowWithToleranceEnabled_SkipsRowAndReportsCount()
        {
            var client = ClientFor(PageWithOneBadRow, tolerateMalformedRows: true);

            var page = await client.GetDataAsync<PaginatedResponse<TransferData>>("https://example.invalid/transfers");

            Assert.NotNull(page);
            Assert.Equal(1, page!.SkippedItemCount);
            Assert.Equal(2, page.Data.Count);
            Assert.All(page.Data, row => Assert.NotNull(row));           // no null placeholders left behind
            Assert.Equal(new[] { "aaaa", "cccc" }, page.Data.Select(r => r.DeployHash));
            Assert.Equal(1, page.PageCount);                              // envelope still parsed
        }

        [Fact]
        public async Task GetDataAsync_MalformedEnvelopeWithToleranceEnabled_StillThrows()
        {
            // page_count sits outside the data array. A broken envelope means the response cannot be
            // trusted at all — tolerating rows must not quietly downgrade that into a partial page.
            const string badEnvelope = @"{
                ""data"": [ { ""deploy_hash"": ""aaaa"", ""block_height"": 900 } ],
                ""item_count"": 1,
                ""page_count"": ""not-a-number""
            }";
            var client = ClientFor(badEnvelope, tolerateMalformedRows: true);

            await Assert.ThrowsAnyAsync<JsonException>(() =>
                client.GetDataAsync<PaginatedResponse<TransferData>>("https://example.invalid/transfers"));
        }

        [Fact]
        public async Task GetDataAsync_CleanPageWithToleranceEnabled_ReportsNoSkips()
        {
            var client = ClientFor(CleanPage, tolerateMalformedRows: true);

            var page = await client.GetDataAsync<PaginatedResponse<TransferData>>("https://example.invalid/transfers");

            Assert.NotNull(page);
            Assert.Equal(0, page!.SkippedItemCount);
            Assert.Single(page.Data);
        }

        [Fact]
        public async Task GetDataAsync_HighPrecisionRateWithToleranceEnabled_KeepsFullPrecision()
        {
            // Regression: the tolerant path builds a JToken, and Newtonsoft's default
            // FloatParseHandling.Double would round every number on the way into the tree — so
            // enabling tolerance silently undid the decimal typing this library exists to provide.
            // The two features have to hold at the same time, which the direct-deserialize test
            // below cannot show because it never builds a JToken.
            const string body = @"{
                ""data"": [ { ""deploy_hash"": ""aaaa"", ""rate"": 0.10000000000000000555 } ],
                ""item_count"": 1,
                ""page_count"": 1
            }";
            var client = ClientFor(body, tolerateMalformedRows: true);

            var page = await client.GetDataAsync<PaginatedResponse<TransferData>>("https://example.invalid/transfers");

            Assert.NotNull(page);
            Assert.Equal(0.10000000000000000555m, page!.Data.Single().Rate);
        }

        [Fact]
        public void TransferData_HighPrecisionRate_RoundTripsExactly()
        {
            // 0.10000000000000000555 is the double nearest to 0.1 — if Rate were float or double this
            // would come back as that artefact instead of the exact figure the API sent. Rates are
            // multiplied by transfer amounts to produce fiat values, so the error is not academic.
            const string json = @"{ ""deploy_hash"": ""aaaa"", ""rate"": 0.10000000000000000555 }";

            var transfer = JsonConvert.DeserializeObject<TransferData>(json);

            Assert.NotNull(transfer);
            Assert.Equal(0.10000000000000000555m, transfer!.Rate);
            Assert.NotEqual(0.1m, transfer.Rate);
        }
    }
}
