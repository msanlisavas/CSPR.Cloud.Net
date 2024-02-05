using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Objects.AccountInfo
{
    public class InfoData
    {
        [JsonProperty("nodes")]
        public List<NodeData> Nodes { get; set; }

        [JsonProperty("owner")]
        public OwnerData Owner { get; set; }
    }
}
