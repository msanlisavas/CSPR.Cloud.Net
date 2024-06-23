using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Nft
{
    public class NFTAccountFilterParameters
    {
        [JsonProperty("from_block_height")]
        public string FromBlockHeight { get; set; } // From block height range
        [JsonProperty("to_block_height")]
        public string ToBlockHeight { get; set; } // To block height range
        [JsonProperty("is_burned")]
        public bool IsBurned { get; set; } // Filters tokens by their is_burned status: false to get active NFTs, true to get the burned ones. By default all tokens are returned.
    }
}
