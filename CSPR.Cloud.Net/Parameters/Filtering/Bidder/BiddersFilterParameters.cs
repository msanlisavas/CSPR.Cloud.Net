using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Filtering.Bidder
{
    public class BiddersFilterParameters
    {
        [JsonProperty("era_id")]
        public string EraId { get; set; }
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
        [JsonProperty("public_key")]
        public List<string> PublicKey { get; set; } = new List<string>();
    }
}
