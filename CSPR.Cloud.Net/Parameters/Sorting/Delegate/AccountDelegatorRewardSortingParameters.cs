using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Delegate
{
    public class AccountDelegatorRewardSortingParameters : BaseSortingParameters
    {
        [JsonProperty("era_id")]
        public bool OrderByEraId { get; set; } = false;

    }
}
