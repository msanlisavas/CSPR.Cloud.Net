using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Block
{
    public class BlockSortingParameters : BaseSortingParameters
    {
        [JsonProperty("block_height")]
        public bool OrderByBlockHeight { get; set; } = false;

        [JsonProperty("timestamp")]
        public bool OrderByTimestamp { get; set; } = false;

    }
}
