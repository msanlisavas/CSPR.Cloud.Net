using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Account
{
    public class AccountData
    {
        [JsonProperty("account_hash")]
        public string AccountHash { get; set; }

        [JsonProperty("balance")]
        public ulong? Balance { get; set; }

        [JsonProperty("main_purse_uref")]
        public string MainPurseUref { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("auction_status")]
        public string AuctionStatus { get; set; }

        [JsonProperty("delegated_balance")]
        public ulong? DelegatedBalance { get; set; }

        [JsonProperty("undelegated_balance")]
        public ulong? UndelegatedBalance { get; set; }
        [JsonProperty("staked_balance")]
        public ulong? StakedBalance { get; set; }
        [JsonProperty("account_info")]
        public AccountInfoData AccountInfo { get; set; }
        [JsonProperty("centralized_account_info")]
        public CentralizedAccountInfoData CentralizedAccountInfo { get; set; }

    }
}
