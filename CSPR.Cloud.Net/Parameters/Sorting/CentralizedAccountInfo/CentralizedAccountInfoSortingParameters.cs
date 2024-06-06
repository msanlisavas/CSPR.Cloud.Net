using CSPR.Cloud.Net.Enums;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.CentralizedAccountInfo
{
    public class CentralizedAccountInfoSortingParameters
    {
        [JsonProperty("account_hash")]
        public bool OrderByAccountHash { get; set; } = false;


        [JsonProperty("sort_type")]
        public SortType SortType { get; set; } = SortType.Descending;
    }
}
