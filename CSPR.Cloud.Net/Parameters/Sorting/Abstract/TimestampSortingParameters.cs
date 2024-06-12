using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Abstract
{
    public class TimestampSortingParameters : BaseSortingParameters
    {
        [JsonProperty("timestamp")]
        public bool OrderByTimestamp { get; set; } = false;
    }
}
