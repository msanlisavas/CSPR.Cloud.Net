using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.X402
{
    /// <summary>
    /// Request body of both <c>POST /verify</c> and <c>POST /settle</c>. Per the CSPR.cloud
    /// facilitator docs the protocol version lives INSIDE <c>paymentPayload</c>, not at the top
    /// level.
    /// </summary>
    public class X402FacilitatorRequest
    {
        [JsonProperty("paymentPayload")]
        public X402PaymentPayload PaymentPayload { get; set; }

        [JsonProperty("paymentRequirements")]
        public X402PaymentRequirements PaymentRequirements { get; set; }
    }
}
