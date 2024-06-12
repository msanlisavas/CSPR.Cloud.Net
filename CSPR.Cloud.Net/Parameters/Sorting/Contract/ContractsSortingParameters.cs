using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Contract
{
    public class ContractsSortingParameters : BaseSortingParameters
    {
        [JsonProperty("timestamp")]
        public bool OrderByTimestamp { get; set; } = false;

    }
}
