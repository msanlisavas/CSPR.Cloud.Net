using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Filtering.Validator
{
    public class ValidatorHistoricalPerformanceFilterParameters
    {
        [JsonProperty("era_id")]
        public List<string> EraIds { get; set; } // Current era validator identifier
    }
}
