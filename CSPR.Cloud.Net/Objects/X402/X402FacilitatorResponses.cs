using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CSPR.Cloud.Net.Objects.X402
{
    /// <summary>Response of <c>POST /verify</c>. Verification commits no state.</summary>
    public class X402VerifyResponse
    {
        [JsonProperty("isValid")]
        public bool? IsValid { get; set; }

        /// <summary>The payer in account-hash / tagged-address form — NOT the public key.</summary>
        [JsonProperty("payer")]
        public string Payer { get; set; }

        [JsonProperty("invalidReason")]
        public string InvalidReason { get; set; }

        [JsonProperty("invalidMessage")]
        public string InvalidMessage { get; set; }

        [JsonProperty("extensions")]
        public JToken Extensions { get; set; }
    }

    /// <summary>
    /// Response of <c>POST /settle</c>. The facilitator answers HTTP 200 even for a failed
    /// settlement — <see cref="Success"/> is the verdict, never the status code.
    /// </summary>
    public class X402SettleResponse
    {
        [JsonProperty("success")]
        public bool? Success { get; set; }

        /// <summary>Deploy hash of the on-chain settlement; empty when nothing was broadcast.</summary>
        [JsonProperty("transaction")]
        public string Transaction { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; }

        [JsonProperty("payer")]
        public string Payer { get; set; }

        [JsonProperty("errorReason")]
        public string ErrorReason { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Response of <c>GET /supported</c> — scheme/network support and facilitator signer
    /// accounts. It does NOT list tokens; the asset is the resource server's own choice.
    /// </summary>
    public class X402SupportedResponse
    {
        [JsonProperty("kinds")]
        public List<X402SupportedKind> Kinds { get; set; } = new List<X402SupportedKind>();

        [JsonProperty("extensions")]
        public JToken Extensions { get; set; }

        [JsonProperty("signers")]
        public Dictionary<string, List<string>> Signers { get; set; } =
            new Dictionary<string, List<string>>();
    }

    public class X402SupportedKind
    {
        [JsonProperty("x402Version")]
        public int? X402Version { get; set; }

        [JsonProperty("scheme")]
        public string Scheme { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; }

        [JsonProperty("extra")]
        public X402SupportedKindExtra Extra { get; set; }
    }

    public class X402SupportedKindExtra
    {
        [JsonProperty("feePayer")]
        public string FeePayer { get; set; }
    }
}
