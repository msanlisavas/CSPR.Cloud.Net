using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Dex
{
    public class DexData
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
