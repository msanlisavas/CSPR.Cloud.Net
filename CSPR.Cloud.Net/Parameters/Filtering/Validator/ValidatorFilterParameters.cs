using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Validator
{
    public class ValidatorFilterParameters
    {
        [JsonProperty("era_id")]
        public string EraId { get; set; } // Current era validator identifier
    }
}
