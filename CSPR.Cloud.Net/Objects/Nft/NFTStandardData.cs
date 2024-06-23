using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Nft
{
    public class NFTStandardData
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
