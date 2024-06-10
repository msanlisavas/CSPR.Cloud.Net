using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Args
{
    public class TargetData
    {
        [JsonProperty("cl_type")]
        public ClTypeByteArrayData ClType { get; set; }

        [JsonProperty("parsed")]
        public string Parsed { get; set; }
    }
}
