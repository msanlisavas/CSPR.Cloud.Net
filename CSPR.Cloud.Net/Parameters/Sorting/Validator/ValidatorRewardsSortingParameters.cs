using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Validator
{
    public class ValidatorRewardsSortingParameters : BaseSortingParameters
    {
        [JsonProperty("era_id")]
        public bool OrderByEraId { get; set; }
    }
}
