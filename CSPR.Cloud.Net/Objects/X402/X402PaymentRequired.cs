using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CSPR.Cloud.Net.Objects.X402
{
    /// <summary>
    /// The x402 v2 PaymentRequired object — the value a resource server base64-encodes into the
    /// <c>PAYMENT-REQUIRED</c> response header of a 402.
    /// </summary>
    public class X402PaymentRequired
    {
        [JsonProperty("x402Version")]
        public int X402Version { get; set; } = 2;

        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }

        [JsonProperty("resource")]
        public X402Resource Resource { get; set; }

        [JsonProperty("accepts")]
        public List<X402PaymentRequirements> Accepts { get; set; } = new List<X402PaymentRequirements>();

        [JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
        public JToken Extensions { get; set; }
    }

    public class X402Resource
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("mimeType", NullValueHandling = NullValueHandling.Ignore)]
        public string MimeType { get; set; }

        [JsonProperty("serviceName", NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceName { get; set; }
    }
}
