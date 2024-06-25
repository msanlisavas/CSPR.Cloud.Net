using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Filtering.Validator
{
    public class ValidatorHistoricalAveragePerformanceFilterParameters
    {
        [JsonProperty("era_id")]
        public List<string> EraIds { get; set; }
    }
}
