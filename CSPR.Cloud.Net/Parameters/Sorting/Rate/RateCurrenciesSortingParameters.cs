using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Rate
{
    public class RateCurrenciesSortingParameters : BaseSortingParameters
    {
        [JsonProperty("id")]
        public bool OrderById { get; set; } = false;
    }
}
