using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Nft
{
    /// <summary>
    /// Represents optional parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/optional-properties">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class NFTAccountActionsOptionalParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include contract package information. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("contract_package")]
        public bool ContractPackage { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include deploy information. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("deploy")]
        public bool Deploy { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include the 'from' public key. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("from_public_key")]
        public bool FromPublicKey { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include the 'to' public key. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("to_public_key")]
        public bool ToPublicKey { get; set; } = false;

    }
}
