using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Nft
{

    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class NFTContractPackageOwnershipFilterParameters
    {
        /// <summary>
        /// Filters by owner hash.
        /// </summary>
        [JsonProperty("owner_hash")]
        public string OwnerHash { get; set; }

    }
}
