using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Block
{
    /// <summary>
    /// Represents optional parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/optional-properties">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class BlockOptionalParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include proposer account info. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("proposer_account_info")]
        public bool ProposerAccountInfo { get; set; } = false;

        /// <summary>
        /// Includes proposer centralized account info (v2.1.0+).
        /// </summary>
        [JsonProperty("proposer_centralized_account_info")]
        public bool ProposerCentralizedAccountInfo { get; set; } = false;

        /// <summary>
        /// Includes the proposer's registered CSPR.name when available (v2.1.0+).
        /// </summary>
        [JsonProperty("proposer_cspr_name")]
        public bool ProposerCsprName { get; set; } = false;

    }
}
