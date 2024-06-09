using CSPR.Cloud.Net.Enums;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Delegate
{
    public class AccountDelegatorRewardSortingParameters
    {
        [JsonProperty("era_id")]
        public bool OrderByEraId { get; set; } = false;


        [JsonProperty("sort_type")]
        public SortType SortType { get; set; } = SortType.Descending;
    }
}
