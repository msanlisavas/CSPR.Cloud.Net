using CSPR.Cloud.Net.Enums;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Account
{
    public class AccountsSortingParameters
    {
        [JsonProperty("balance")]
        public bool OrderByBalance { get; set; } = false;

        [JsonProperty("total_balance")]
        public bool OrderByTotalBalance { get; set; } = false;


        [JsonProperty("sort_type")]
        public SortType SortType { get; set; } = SortType.Descending;
    }
}
