using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Account
{
    public class AccountInfosSortingParameters : BaseSortingParameters
    {
        [JsonProperty("account_hash")]
        public bool OrderByAccountHash { get; set; }
    }
}
