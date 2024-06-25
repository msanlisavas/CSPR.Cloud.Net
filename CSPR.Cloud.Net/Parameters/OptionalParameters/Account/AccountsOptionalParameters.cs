using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Account
{
    public class AccountsOptionalParameters
    {
        [JsonProperty("auction_status")]
        public bool AuctionStatus { get; set; } = false;

        [JsonProperty("delegated_balance")]
        public bool DelegatedBalance { get; set; } = false;

        [JsonProperty("undelegating_balance")]
        public bool UndelegatingBalance { get; set; } = false;

        [JsonProperty("staked_balance")]
        public bool StakedBalance { get; set; } = false;
        [JsonProperty("account_info")]
        public bool AccountInfo { get; set; } = false;
        [JsonProperty("centralized_account_info")]
        public bool CentralizedAccountInfo { get; set; } = false;
    }

}
