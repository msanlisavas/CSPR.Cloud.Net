using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Nft
{
    public class NFTContractPackageActionsFilterParameters
    {
        [JsonProperty("from_block_height")]
        public string FromBlockHeight { get; set; } // From block height range
        [JsonProperty("to_block_height")]
        public string ToBlockHeight { get; set; } // To block height range
    }
}
