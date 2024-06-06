using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Bidder
{
    public class BidderOptionalParameters
    {
        [JsonProperty("account_info")]
        public bool AccountInfo { get; set; } = false;
        [JsonProperty("average_performance")]
        public bool AveragePerformance { get; set; } = false;
        [JsonProperty("centralized_account_info")]
        public bool CentralizedAccountInfo { get; set; } = false;
    }
}
