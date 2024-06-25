using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Filtering.Validator
{
    public class ValidatorsHistoricalAveragePerformanceFilterParameters
    {
        [JsonProperty("era_id")]
        public List<string> EraIds { get; set; }
        [JsonProperty("public_key")]
        public List<string> PublicKeys { get; set; }
    }
}
