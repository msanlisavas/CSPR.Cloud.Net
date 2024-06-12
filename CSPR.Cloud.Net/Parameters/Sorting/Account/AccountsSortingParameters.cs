using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Account
{
    public class AccountsSortingParameters : BaseSortingParameters
    {
        [JsonProperty("balance")]
        public bool OrderByBalance { get; set; } = false;

        [JsonProperty("total_balance")]
        public bool OrderByTotalBalance { get; set; } = false;

    }
}
