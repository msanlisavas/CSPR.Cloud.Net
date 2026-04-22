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

        /// <summary>
        /// Restricts the result set to eras at or after the given era id (v2.4.0+).
        /// </summary>
        [JsonProperty("from_era_id")]
        public string FromEraId { get; set; }

        /// <summary>
        /// Restricts the result set to eras at or before the given era id (v2.4.0+).
        /// </summary>
        [JsonProperty("to_era_id")]
        public string ToEraId { get; set; }
    }
}
