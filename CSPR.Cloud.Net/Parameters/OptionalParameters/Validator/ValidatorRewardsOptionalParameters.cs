using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Validator
{
    public class ValidatorRewardsOptionalParameters
    {
        [JsonProperty("rate")]
        public int? Rate { get; set; } = 0;
    }
}
