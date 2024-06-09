using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Delegate
{
    public class DelegatorRewardData
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [JsonProperty("era_id")]
        public int? EraId { get; set; }
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }
        [JsonProperty("validator_public_key")]
        public string ValidatorPublicKey { get; set; }
        [JsonProperty("rate")]
        public float? Rate { get; set; }
        [JsonProperty("account_info")]
        public AccountInfoData AccountInfo { get; set; }
        [JsonProperty("centralized_account_info")]
        public CentralizedAccountInfoData CentralizedAccountInfo { get; set; }
    }
}
