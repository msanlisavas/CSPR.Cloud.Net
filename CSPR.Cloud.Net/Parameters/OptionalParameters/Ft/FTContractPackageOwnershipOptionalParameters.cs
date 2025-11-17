using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Ft
{
    /// <summary>
    /// Represents optional parameters for contract package fungible token ownership endpoint.
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/optional-properties">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class FTContractPackageOwnershipOptionalParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include contract package. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("contract_package")]
        public bool ContractPackage { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include account info. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("account_info")]
        public bool AccountInfo { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include centralized account info. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("centralized_account_info")]
        public bool CentralizedAccountInfo { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include owner CSPR name. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("owner_cspr_name")]
        public bool OwnerCsprName { get; set; } = false;
    }
}
