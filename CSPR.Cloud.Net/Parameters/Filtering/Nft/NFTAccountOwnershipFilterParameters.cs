using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Nft
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class NFTAccountOwnershipFilterParameters
    {
        /// <summary>
        /// Filters by contract package hash.
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

    }
}
