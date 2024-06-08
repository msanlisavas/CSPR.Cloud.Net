using CSPR.Cloud.Net.Enums;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Contract
{
    public class ContractPackageSortingParameters
    {

        [JsonProperty("timestamp")]
        public bool OrderByTimestamp { get; set; } = false;


        [JsonProperty("sort_type")]
        public SortType SortType { get; set; } = SortType.Descending;
    }
}
