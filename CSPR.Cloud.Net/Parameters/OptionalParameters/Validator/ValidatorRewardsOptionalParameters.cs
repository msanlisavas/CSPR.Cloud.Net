using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Validator
{
    /// <summary>
    /// Represents optional parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/optional-properties">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class ValidatorRewardsOptionalParameters
    {
        /// <summary>
        /// Gets or sets the rate that was relevant at the moment when the last block was proposed.
        /// To include the USD to CSPR rate, pass the USD currency ID (1) as a parameter to the rate function.
        /// For more details, see <see href="https://docs.cspr.cloud/documentation/overview/optional-properties#functions">Including CSPR rates</see>.
        /// </summary>
        [JsonProperty("rate")]
        public int? Rate { get; set; } = 0;

        /// <summary>
        /// Includes validator account info on each reward record.
        /// </summary>
        [JsonProperty("account_info")]
        public bool AccountInfo { get; set; } = false;

        /// <summary>
        /// Includes validator centralized account info on each reward record.
        /// </summary>
        [JsonProperty("centralized_account_info")]
        public bool CentralizedAccountInfo { get; set; } = false;

        /// <summary>
        /// Includes the validator's registered CSPR.name when available (v2.1.0+).
        /// </summary>
        [JsonProperty("cspr_name")]
        public bool CsprName { get; set; } = false;

    }
}
