using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.X402
{
    /// <summary>
    /// The x402 v2 PaymentPayload — what a payer base64-encodes into the
    /// <c>PAYMENT-SIGNATURE</c> request header. <c>payload</c> is the Casper exact-scheme shape:
    /// a CEP-3009 authorization plus the signer's tagged public key and signature over the
    /// CEP-3009 EIP-712 digest.
    /// </summary>
    public class X402PaymentPayload
    {
        [JsonProperty("x402Version")]
        public int? X402Version { get; set; }

        /// <summary>Optional echo of the challenge's resource — tolerated, not required.</summary>
        [JsonProperty("resource", NullValueHandling = NullValueHandling.Ignore)]
        public X402Resource Resource { get; set; }

        [JsonProperty("accepted")]
        public X402PaymentRequirements Accepted { get; set; }

        [JsonProperty("payload")]
        public X402CasperExactPayload Payload { get; set; }
    }

    public class X402CasperExactPayload
    {
        [JsonProperty("authorization")]
        public X402CasperAuthorization Authorization { get; set; }

        /// <summary>1-byte algorithm tag (01 ed25519, 02 secp256k1) + key bytes, hex.</summary>
        [JsonProperty("publicKey")]
        public string PublicKey { get; set; }

        /// <summary>Signature over the CEP-3009 EIP-712 digest, hex.</summary>
        [JsonProperty("signature")]
        public string Signature { get; set; }
    }

    public class X402CasperAuthorization
    {
        /// <summary>33-byte tagged address of the payer, hex.</summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>33-byte tagged address of the recipient, hex.</summary>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>U256 atomic-unit decimal string.</summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>Unix seconds as a DECIMAL STRING; "0" = no lower bound. The facilitator's Go
        /// parser types this as a string and rejects JSON numbers with
        /// invalid_exact_casper_malformed_payload (live-verified 2026-08-20).</summary>
        [JsonProperty("validAfter")]
        public string ValidAfter { get; set; }

        /// <summary>Unix seconds as a DECIMAL STRING — same wire rule as <see cref="ValidAfter"/>.</summary>
        [JsonProperty("validBefore")]
        public string ValidBefore { get; set; }

        /// <summary>32-byte nonce, hex — CEP-3009's per-authorizer replay key.</summary>
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
    }
}
