using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Delegate
{
    public class DelegationSortingParameters : BaseSortingParameters
    {
        [JsonProperty("stake")]
        public bool OrderByStake { get; set; } = false;


    }
}
