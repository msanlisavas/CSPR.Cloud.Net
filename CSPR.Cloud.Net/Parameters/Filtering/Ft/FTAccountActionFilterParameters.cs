using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Ft
{
    public class FTAccountActionFilterParameters
    {
        [JsonProperty("from_block_height")]
        public string FromBlockHeight { get; set; }
        [JsonProperty("to_block_height")]
        public string ToBlockHeight { get; set; }
    }
}
