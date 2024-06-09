using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Delegate
{
    public class DelegationOptionalParameters
    {
        [JsonProperty("account_info")]
        public bool AccountInfo { get; set; } = false;
        [JsonProperty("validator_account_info")]
        public bool ValidatorAccountInfo { get; set; } = false;
        [JsonProperty("centralized_account_info")]
        public bool CentralizedAccountInfo { get; set; } = false;
    }
}
