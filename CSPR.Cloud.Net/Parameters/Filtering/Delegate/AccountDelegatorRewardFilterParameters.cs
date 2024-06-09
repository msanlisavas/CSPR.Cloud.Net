using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Delegate
{
    public class AccountDelegatorRewardFilterParameters
    {
        [JsonProperty("validator_public_key")]
        public string ValidatorPublicKey { get; set; }
    }
}
