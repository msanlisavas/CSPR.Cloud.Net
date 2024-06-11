using CSPR.Cloud.Net.Enums;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Deploy
{
    public class DeploysSortingParameters
    {
        [JsonProperty("timestamp")]
        public bool OrderByTimestamp { get; set; } = false;


        [JsonProperty("sort_type")]
        public SortType SortType { get; set; } = SortType.Descending;
    }
}
