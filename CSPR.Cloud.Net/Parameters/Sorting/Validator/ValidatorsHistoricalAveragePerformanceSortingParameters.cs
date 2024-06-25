using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Validator
{
    public class ValidatorsHistoricalAveragePerformanceSortingParameters : BaseSortingParameters
    {
        [JsonProperty("era_id")]
        public bool OrderByEraId { get; set; }
        [JsonProperty("public_key")]
        public bool OrderByPublicKeyPerformance { get; set; } // Sort performances by validator public key
    }
}
