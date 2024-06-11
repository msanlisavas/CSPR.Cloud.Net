using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Objects.Args
{
    public class ArgsData
    {
        [JsonExtensionData]
        public Dictionary<string, JToken> Properties { get; set; }
    }
}
