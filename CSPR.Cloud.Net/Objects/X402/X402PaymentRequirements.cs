using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.X402
{
    /// <summary>
    /// One entry of a 402 challenge's <c>accepts</c> array — and the object a payer echoes back
    /// as <c>accepted</c> in its payment payload. Field meanings follow the x402 v2 Casper
    /// "exact" scheme. Note x402 protocol JSON is camelCase, unlike the CSPR.cloud REST API.
    /// </summary>
    public class X402PaymentRequirements
    {
        [JsonProperty("scheme")]
        public string Scheme { get; set; }

        /// <summary>CAIP-2 network id, e.g. "casper:casper" or "casper:casper-test".</summary>
        [JsonProperty("network")]
        public string Network { get; set; }

        /// <summary>Atomic-unit decimal string — never a float.</summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>32-byte CEP-18 contract package hash, hex.</summary>
        [JsonProperty("asset")]
        public string Asset { get; set; }

        /// <summary>33-byte tagged address (1-byte type tag + 32-byte hash), hex.</summary>
        [JsonProperty("payTo")]
        public string PayTo { get; set; }

        [JsonProperty("maxTimeoutSeconds")]
        public long? MaxTimeoutSeconds { get; set; }

        /// <summary>REQUIRED by the Casper scheme: carries the CEP-3009 EIP-712 signing-domain
        /// fields the payer needs to construct the digest.</summary>
        [JsonProperty("extra")]
        public X402PaymentRequirementsExtra Extra { get; set; }
    }

    public class X402PaymentRequirementsExtra
    {
        /// <summary>CEP-18 token name — the "name" field of the CEP-3009 EIP-712 domain separator.</summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>Signing domain version.</summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("decimals", NullValueHandling = NullValueHandling.Ignore)]
        public int? Decimals { get; set; }

        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }
    }
}
