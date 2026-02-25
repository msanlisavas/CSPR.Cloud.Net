using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.CsprName
{
    public class CsprNameResolutionData
    {
        [JsonProperty("name_token_id")]
        public string NameTokenId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("resolved_hash")]
        public string ResolvedHash { get; set; }

        [JsonProperty("is_primary")]
        public bool? IsPrimary { get; set; }

        [JsonProperty("expires_at")]
        public string ExpiresAt { get; set; }
    }
}
