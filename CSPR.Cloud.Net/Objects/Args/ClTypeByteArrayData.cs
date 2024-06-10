using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Args
{
    public class ClTypeByteArrayData
    {
        [JsonProperty("ByteArray")]
        public int ByteArray { get; set; }
    }
}
