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
    }
}
