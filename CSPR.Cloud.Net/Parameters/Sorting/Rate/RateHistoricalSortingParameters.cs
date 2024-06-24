using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Rate
{
    public class RateHistoricalSortingParameters : BaseSortingParameters
    {
        [JsonProperty("created")]
        public bool OrderByCreated { get; set; } = false;
    }
}
