using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Delegate
{
    public class AccountDelegatorRewardOptionalParameters
    {
        [JsonProperty("rate")]
        public int Rate { get; set; } = 0;
        [JsonProperty("account_info")]
        public bool AccountInfo { get; set; } = false;
        [JsonProperty("centralized_account_info")]
        public bool CentralizedAccountInfo { get; set; } = false;
    }
}
