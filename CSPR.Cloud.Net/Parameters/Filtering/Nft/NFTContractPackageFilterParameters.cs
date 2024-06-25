using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Nft
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class NFTContractPackageFilterParameters
    {
        /// <summary>
        /// Filters by the starting block height range.
        /// </summary>
        [JsonProperty("from_block_height")]
        public string FromBlockHeight { get; set; } // From block height range

        /// <summary>
        /// Filters by the ending block height range.
        /// </summary>
        [JsonProperty("to_block_height")]
        public string ToBlockHeight { get; set; } // To block height range

        /// <summary>
        /// Filters tokens by their burned status: false to get active NFTs, true to get the burned ones. By default, all tokens are returned.
        /// </summary>
        [JsonProperty("is_burned")]
        public bool IsBurned { get; set; } // Filters tokens by their is_burned status: false to get active NFTs, true to get the burned ones. By default all tokens are returned.

    }
}
