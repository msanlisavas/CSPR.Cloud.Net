using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Parameters.Filtering.Rate
{
    public class RateHistoricalFilterParameters
    {
        [JsonProperty("from")]
        public DateTime? From { get; set; }
        [JsonProperty("to")]
        public DateTime? To { get; set; }
    }
}
