using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Args
{
    public class AmountData
    {
        [JsonProperty("cl_type")]
        public string ClType { get; set; }

        [JsonProperty("parsed")]
        public string Parsed { get; set; }
    }
}
