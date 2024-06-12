using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.CentralizedAccountInfo
{
    public class CentralizedAccountInfoSortingParameters : BaseSortingParameters
    {
        [JsonProperty("account_hash")]
        public bool OrderByAccountHash { get; set; } = false;
    }
}
