using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Ft
{
    public class FTActionTypeData
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
