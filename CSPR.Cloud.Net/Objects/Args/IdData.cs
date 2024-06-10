using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Args
{
    public class IdData
    {
        [JsonProperty("cl_type")]
        public ClTypeOptionData ClType { get; set; }

        [JsonProperty("parsed")]
        public object Parsed { get; set; } // Using object to handle potential null value
    }
}
