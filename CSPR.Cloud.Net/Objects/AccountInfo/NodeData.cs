using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Objects.AccountInfo
{
    public class NodeData
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("functionality")]
        public List<string> Functionality { get; set; }

        [JsonProperty("location")]
        public LocationData Location { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
    }
}
