using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Validator
{
    public class RelativeValidatorPerformanceData
    {
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("era_id")]
        public uint? EraId { get; set; }

        [JsonProperty("score")]
        public double? Score { get; set; }
    }
}
