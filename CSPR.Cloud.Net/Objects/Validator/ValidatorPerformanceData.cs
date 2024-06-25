using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Validator
{
    public class ValidatorPerformanceData
    {
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("era_id")]
        public uint? EraId { get; set; }

        [JsonProperty("average_score")]
        public double? AverageScore { get; set; }
    }
}
