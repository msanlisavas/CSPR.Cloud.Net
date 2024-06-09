using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Delegate
{
    public class DelegationData
    {
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
        [JsonProperty("validator_public_key")]
        public string ValidatorPublicKey { get; set; }
        [JsonProperty("stake")]
        public string Stake { get; set; }
        [JsonProperty("bonding_purse")]
        public string BondingPurse { get; set; }
        [JsonProperty("account_info")]
        public AccountInfoData AccountInfo { get; set; }
        [JsonProperty("validator_account_info")]
        public AccountInfoData ValidatorAccountInfo { get; set; }
        [JsonProperty("centralized_account_info")]
        public CentralizedAccountInfoData CentralizedAccountInfo { get; set; }

    }
}
