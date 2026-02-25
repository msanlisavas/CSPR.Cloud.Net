using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Ft
{
    public class FTDailyRateSortingParameters : BaseSortingParameters
    {
        [JsonProperty("date")]
        public bool OrderByDate { get; set; } = false;
    }
}
