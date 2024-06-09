using CSPR.Cloud.Net.Enums;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Delegate
{
    public class DelegationSortingParameters
    {
        [JsonProperty("stake")]
        public bool Stake { get; set; } = false;


        [JsonProperty("sort_type")]
        public SortType SortType { get; set; } = SortType.Descending;
    }
}
