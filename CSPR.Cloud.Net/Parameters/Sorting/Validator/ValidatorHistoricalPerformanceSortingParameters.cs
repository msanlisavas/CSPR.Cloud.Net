using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Validator
{
    public class ValidatorHistoricalPerformanceSortingParameters : BaseSortingParameters
    {
        [JsonProperty("era_id")]
        public bool OrderByEraId { get; set; } // Current era validator identifier
    }
}
