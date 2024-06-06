using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.Validator;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Bidder
{
    public class BidderData
    {

        [JsonProperty("fee")]
        public int Fee { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("network_share")]
        public string NetworkShare { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("self_share")]
        public string SelfShare { get; set; }

        [JsonProperty("self_stake")]
        public ulong SelfStake { get; set; }

        [JsonProperty("total_stake")]
        public ulong TotalStake { get; set; }
        [JsonProperty("account_info")]
        public AccountInfoData AccountInfo { get; set; }
        [JsonProperty("average_performance")]
        public ValidatorPerformanceData AveragePerformance { get; set; }
        [JsonProperty("centralized_account_info")]
        public CentralizedAccountInfo CentralizedAccountInfo { get; set; }
    }
}
