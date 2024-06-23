using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Nft
{
    public class NFTMetadataStatusData
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
