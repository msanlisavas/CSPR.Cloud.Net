using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Nft
{
    /// <summary>
    /// Represents optional parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/optional-properties">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class NFTAccountOwnershipOptionalParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include contract package information. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("contract_package")]
        public bool ContractPackage { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include the owner's public key. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("owner_public_key")]
        public bool OwnerPublicKey { get; set; } = false;

    }
}
