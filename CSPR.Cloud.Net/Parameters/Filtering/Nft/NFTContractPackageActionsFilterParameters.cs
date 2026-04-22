using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Nft
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class NFTContractPackageActionsFilterParameters
    {
        /// <summary>
        /// Filters by NFT action type identifier (v2.0.20+). See <c>/nft-token-action-types</c> for valid IDs.
        /// </summary>
        [JsonProperty("nft_action_id")]
        public string NftActionId { get; set; }

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
