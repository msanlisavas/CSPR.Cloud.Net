using System.Linq;
using System.Net;
using System.Text;
using CSPR.Cloud.Net.Objects.X402;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace CSPR.Cloud.Net.Tests
{
    /// <summary>
    /// Offline tests for the x402 facilitator client (new in v4.2.0): protocol-object
    /// serialization pinned against the x402 v2 / Casper exact-scheme JSON shapes, URL building,
    /// transport behavior via stub handlers. No network, no API key.
    /// </summary>
    public class CSPRCloudNetX402Tests
    {
        private const string CanonicalRequirementsJson =
            "{\"scheme\":\"exact\",\"network\":\"casper:casper-test\",\"amount\":\"25000\"," +
            "\"asset\":\"0101010101010101010101010101010101010101010101010101010101010101\"," +
            "\"payTo\":\"000202020202020202020202020202020202020202020202020202020202020202\"," +
            "\"maxTimeoutSeconds\":300,\"extra\":{\"name\":\"csprUSD\",\"version\":\"1\"}}";

        [Fact]
        public void PaymentRequirements_RoundTrips_CamelCaseJson()
        {
            var parsed = JsonConvert.DeserializeObject<X402PaymentRequirements>(CanonicalRequirementsJson);

            Assert.NotNull(parsed);
            Assert.Equal("exact", parsed!.Scheme);
            Assert.Equal("casper:casper-test", parsed.Network);
            Assert.Equal("25000", parsed.Amount);
            Assert.Equal(300, parsed.MaxTimeoutSeconds);
            Assert.Equal("csprUSD", parsed.Extra.Name);
            Assert.Equal("1", parsed.Extra.Version);

            var reserialized = JObject.Parse(JsonConvert.SerializeObject(parsed,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            Assert.True(JToken.DeepEquals(JObject.Parse(CanonicalRequirementsJson), reserialized),
                reserialized.ToString());
        }

        [Fact]
        public void PaymentPayload_Deserializes_CasperExactScheme()
        {
            var json =
                "{\"x402Version\":2," +
                "\"accepted\":" + CanonicalRequirementsJson + "," +
                "\"payload\":{\"authorization\":{" +
                "\"from\":\"00aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\"," +
                "\"to\":\"000202020202020202020202020202020202020202020202020202020202020202\"," +
                "\"value\":\"25000\",\"validAfter\":0,\"validBefore\":1787200000," +
                "\"nonce\":\"cccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc\"}," +
                "\"publicKey\":\"01bbbb\",\"signature\":\"dddd\"}}";

            var payload = JsonConvert.DeserializeObject<X402PaymentPayload>(json);

            Assert.NotNull(payload);
            Assert.Equal(2, payload!.X402Version);
            Assert.Null(payload.Resource);
            Assert.Equal("25000", payload.Accepted.Amount);
            Assert.Equal("25000", payload.Payload.Authorization.Value);
            Assert.Equal(0L, payload.Payload.Authorization.ValidAfter);
            Assert.Equal(1787200000L, payload.Payload.Authorization.ValidBefore);
            Assert.Equal("01bbbb", payload.Payload.PublicKey);
        }

        [Fact]
        public void PaymentRequired_Serializes_WithVersionAndAccepts()
        {
            var challenge = new X402PaymentRequired
            {
                Resource = new X402Resource { Url = "https://example.invalid/v1/chat", MimeType = "text/event-stream" },
            };
            challenge.Accepts.Add(JsonConvert.DeserializeObject<X402PaymentRequirements>(CanonicalRequirementsJson)!);

            var json = JObject.Parse(JsonConvert.SerializeObject(challenge,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

            Assert.Equal(2, (int)json["x402Version"]!);
            Assert.Equal("https://example.invalid/v1/chat", (string)json["resource"]!["url"]!);
            Assert.Single((JArray)json["accepts"]!);
            Assert.Null(json["error"]);
            Assert.Null(json["extensions"]);
        }

        [Fact]
        public void VerifyAndSettleResponses_Deserialize_DocumentedShapes()
        {
            var verify = JsonConvert.DeserializeObject<X402VerifyResponse>(
                "{\"isValid\":false,\"invalidReason\":\"amount_mismatch\",\"invalidMessage\":\"expected 25000\"}");
            Assert.False(verify!.IsValid);
            Assert.Equal("amount_mismatch", verify.InvalidReason);

            var settle = JsonConvert.DeserializeObject<X402SettleResponse>(
                "{\"success\":true,\"transaction\":\"deadbeef\",\"network\":\"casper:casper-test\"," +
                "\"payer\":\"00aaaa\"}");
            Assert.True(settle!.Success);
            Assert.Equal("deadbeef", settle.Transaction);

            var supported = JsonConvert.DeserializeObject<X402SupportedResponse>(
                "{\"kinds\":[{\"x402Version\":2,\"scheme\":\"exact\",\"network\":\"casper:casper-test\"," +
                "\"extra\":{\"feePayer\":\"account-hash-ffff\"}}],\"extensions\":[]," +
                "\"signers\":{\"casper:*\":[\"account-hash-ffff\"]}}");
            Assert.Single(supported!.Kinds);
            Assert.Equal("exact", supported.Kinds[0].Scheme);
            Assert.Equal("account-hash-ffff", supported.Kinds[0].Extra.FeePayer);
            Assert.Equal("account-hash-ffff", supported.Signers["casper:*"][0]);
        }

        private const string FacilitatorBase = "https://x402-facilitator.cspr.cloud";

        /// <summary>Records the last request (headers + body) and answers with a fixed body.</summary>
        private sealed class RecordingHandler : HttpMessageHandler
        {
            private readonly string _body;
            private readonly HttpStatusCode _status;
            public HttpRequestMessage? LastRequest;
            public string? LastRequestBody;

            public RecordingHandler(string body, HttpStatusCode status = HttpStatusCode.OK)
            {
                _body = body;
                _status = status;
            }

            protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
            {
                LastRequest = request;
                LastRequestBody = request.Content is null ? null : await request.Content.ReadAsStringAsync();
                return new HttpResponseMessage(_status)
                {
                    Content = new StringContent(_body, Encoding.UTF8, "application/json"),
                };
            }
        }

        private static Clients.CasperCloudX402Client ClientOver(RecordingHandler handler) =>
            new Clients.CasperCloudX402Client(
                new Objects.Config.CasperCloudClientConfig("test-key"),
                new System.Net.Http.HttpClient(handler));

        [Fact]
        public void UrlBuilders_ProduceFacilitatorEndpoints()
        {
            Assert.Equal("https://x402-facilitator.cspr.cloud",
                Clients.Api.Endpoints.BaseUrls.X402Facilitator);
            Assert.Equal(FacilitatorBase + "/supported", Clients.Api.Endpoints.X402.GetSupported(FacilitatorBase));
            Assert.Equal(FacilitatorBase + "/verify", Clients.Api.Endpoints.X402.PostVerify(FacilitatorBase));
            Assert.Equal(FacilitatorBase + "/settle", Clients.Api.Endpoints.X402.PostSettle(FacilitatorBase));
        }

        [Fact]
        public async System.Threading.Tasks.Task VerifyAsync_SendsAuthHeaderAndCamelCaseBody_OmittingNulls()
        {
            var handler = new RecordingHandler("{\"isValid\":true,\"payer\":\"00aaaa\"}");
            var client = ClientOver(handler);
            var request = new X402FacilitatorRequest
            {
                PaymentPayload = JsonConvert.DeserializeObject<X402PaymentPayload>(
                    "{\"x402Version\":2,\"accepted\":" + CanonicalRequirementsJson + "," +
                    "\"payload\":{\"authorization\":{\"from\":\"00aa\",\"to\":\"00bb\",\"value\":\"25000\"," +
                    "\"validAfter\":0,\"validBefore\":1787200000,\"nonce\":\"cc\"}," +
                    "\"publicKey\":\"01dd\",\"signature\":\"ee\"}}"),
                PaymentRequirements = JsonConvert.DeserializeObject<X402PaymentRequirements>(CanonicalRequirementsJson),
            };

            var result = await client.VerifyAsync(request);

            Assert.True(result!.IsValid);
            Assert.Equal("test-key", handler.LastRequest!.Headers.GetValues("Authorization").Single());
            Assert.Equal(FacilitatorBase + "/verify", handler.LastRequest.RequestUri!.ToString());
            var sent = JObject.Parse(handler.LastRequestBody!);
            Assert.Equal(2, (int)sent["paymentPayload"]!["x402Version"]!);
            Assert.Equal("25000", (string)sent["paymentRequirements"]!["amount"]!);
            Assert.Null(sent["paymentPayload"]!["resource"]); // nulls omitted
        }

        [Fact]
        public async System.Threading.Tasks.Task SettleAsync_SurfacesFailureBody_WithoutThrowing()
        {
            // The facilitator answers HTTP 200 even for a failed settlement — the body is the verdict.
            var handler = new RecordingHandler(
                "{\"success\":false,\"errorReason\":\"put_deploy_failed\"," +
                "\"errorMessage\":\"node rejected\",\"transaction\":\"\",\"network\":\"casper:casper-test\"}");
            var client = ClientOver(handler);

            var result = await client.SettleAsync(new X402FacilitatorRequest());

            Assert.False(result!.Success);
            Assert.Equal("put_deploy_failed", result.ErrorReason);
            Assert.Equal("", result.Transaction);
            Assert.Equal(FacilitatorBase + "/settle", handler.LastRequest!.RequestUri!.ToString());
        }

        [Fact]
        public async System.Threading.Tasks.Task GetSupportedAsync_ParsesKindsAndSigners()
        {
            var handler = new RecordingHandler(
                "{\"kinds\":[{\"x402Version\":2,\"scheme\":\"exact\",\"network\":\"casper:casper-test\"," +
                "\"extra\":{\"feePayer\":\"account-hash-ffff\"}}],\"extensions\":[]," +
                "\"signers\":{\"casper:*\":[\"account-hash-ffff\"]}}");
            var client = ClientOver(handler);

            var result = await client.GetSupportedAsync();

            Assert.Equal("casper:casper-test", result!.Kinds[0].Network);
            Assert.Equal(FacilitatorBase + "/supported", handler.LastRequest!.RequestUri!.ToString());
        }

        [Fact]
        public async System.Threading.Tasks.Task UnauthorizedResponse_ThrowsUnauthorizedException()
        {
            var handler = new RecordingHandler("{\"error\":\"bad token\"}", HttpStatusCode.Unauthorized);
            var client = ClientOver(handler);

            await Assert.ThrowsAsync<Errors.UnauthorizedException>(
                () => client.GetSupportedAsync());
        }

        [Fact]
        public void BaseUrlOverride_TrimsTrailingSlash()
        {
            var handler = new RecordingHandler("{\"kinds\":[]}");
            var client = new Clients.CasperCloudX402Client(
                new Objects.Config.CasperCloudClientConfig("test-key"),
                new System.Net.Http.HttpClient(handler),
                baseUrl: "https://my-own-facilitator.example/");

            client.GetSupportedAsync().GetAwaiter().GetResult();

            Assert.Equal("https://my-own-facilitator.example/supported",
                handler.LastRequest!.RequestUri!.ToString());
        }

        /// <summary>Answers only after 30 seconds — cancellation is the only fast exit, so a test
        /// that completes has proven the token reached the handler (same technique as
        /// CSPRCloudNetCancellationTests).</summary>
        private sealed class SlowHandler : HttpMessageHandler
        {
            private readonly System.Threading.Tasks.TaskCompletionSource<bool> _entered =
                new System.Threading.Tasks.TaskCompletionSource<bool>(
                    System.Threading.Tasks.TaskCreationOptions.RunContinuationsAsynchronously);

            public System.Threading.Tasks.Task Entered => _entered.Task;

            protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
            {
                _entered.TrySetResult(true);
                await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(30), cancellationToken);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{}", Encoding.UTF8, "application/json"),
                };
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task VerifyAsync_CancelledMidFlight_Throws()
        {
            var handler = new SlowHandler();
            var client = new Clients.CasperCloudX402Client(
                new Objects.Config.CasperCloudClientConfig("test-key"),
                new System.Net.Http.HttpClient(handler));
            using (var cts = new System.Threading.CancellationTokenSource())
            {
                var call = client.VerifyAsync(new X402FacilitatorRequest(), cts.Token);
                await handler.Entered;
                cts.Cancel();
                await Assert.ThrowsAnyAsync<System.OperationCanceledException>(() => call);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task SettleAsync_CancelledMidFlight_Throws()
        {
            var handler = new SlowHandler();
            var client = new Clients.CasperCloudX402Client(
                new Objects.Config.CasperCloudClientConfig("test-key"),
                new System.Net.Http.HttpClient(handler));
            using (var cts = new System.Threading.CancellationTokenSource())
            {
                var call = client.SettleAsync(new X402FacilitatorRequest(), cts.Token);
                await handler.Entered;
                cts.Cancel();
                await Assert.ThrowsAnyAsync<System.OperationCanceledException>(() => call);
            }
        }
    }
}
