using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Nft
{
    /// <summary>
    /// Filtering parameters for the unscoped NFT listing (<c>/nft-tokens</c>).
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class NFTsFilterParameters
    {
        /// <summary>
        /// Filters by contract package hash. Narrows the listing to a specific NFT collection.
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        /// <summary>
        /// Filters by owner hash (account or contract).
        /// </summary>
        [JsonProperty("owner_hash")]
        public string OwnerHash { get; set; }

        /// <summary>
        /// Filters by the starting block height range.
        /// </summary>
        [JsonProperty("from_block_height")]
        public string FromBlockHeight { get; set; }

        /// <summary>
        /// Filters by the ending block height range.
        /// </summary>
        [JsonProperty("to_block_height")]
        public string ToBlockHeight { get; set; }
    }
}
