using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Nft
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class NFTAccountActionsFilterParameters
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

    }
}
