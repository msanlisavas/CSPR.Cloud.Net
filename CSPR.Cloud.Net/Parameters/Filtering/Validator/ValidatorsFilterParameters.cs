using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Filtering.Validator
{
    public class ValidatorsFilterParameters
    {

        [JsonProperty("era_id")]
        public string EraId { get; set; } // Current era validator identifier

        [JsonProperty("is_active")]
        public bool? IsActive { get; set; } // Current era validator identifier

        [JsonProperty("public_key")]
        public List<string> PublicKeys { get; set; } // Current era validator identifier
    }
}
