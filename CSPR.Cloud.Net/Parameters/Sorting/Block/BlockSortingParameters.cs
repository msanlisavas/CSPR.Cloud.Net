using CSPR.Cloud.Net.Enums;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Block
{
    public class BlockSortingParameters
    {
        [JsonProperty("block_height")]
        public bool OrderByBlockHeight { get; set; } = false;

        [JsonProperty("timestamp")]
        public bool OrderByTimestamp { get; set; } = false;
        [JsonProperty("sort_type")]
        public SortType SortType { get; set; } = SortType.Descending;
    }
}
