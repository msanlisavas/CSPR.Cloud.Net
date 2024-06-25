using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Delegate
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class AccountDelegatorRewardFilterParameters
    {
        /// <summary>
        /// Filters by Validator Public Key
        /// </summary>
        [JsonProperty("validator_public_key")]
        public string ValidatorPublicKey { get; set; }
    }
}
