using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Delegate
{
    /// <summary>
    /// Represents optional parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/optional-properties">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class DelegationOptionalParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include account info. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("account_info")]
        public bool AccountInfo { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include validator account info. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("validator_account_info")]
        public bool ValidatorAccountInfo { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include centralized account info. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("centralized_account_info")]
        public bool CentralizedAccountInfo { get; set; } = false;

        /// <summary>
        /// Includes the bidder record for the delegator (v2.1.0+).
        /// </summary>
        [JsonProperty("bidder")]
        public bool Bidder { get; set; } = false;

        /// <summary>
        /// Includes the delegator's registered CSPR.name when available (v2.1.0+).
        /// </summary>
        [JsonProperty("cspr_name")]
        public bool CsprName { get; set; } = false;

        /// <summary>
        /// Includes the validator's registered CSPR.name when available (v2.1.0+).
        /// </summary>
        [JsonProperty("validator_cspr_name")]
        public bool ValidatorCsprName { get; set; } = false;

    }
}
