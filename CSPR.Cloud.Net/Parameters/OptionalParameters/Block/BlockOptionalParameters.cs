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

    }
}
