using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.CentralizedAccountInfo
{
    public class CentralizedAccountInfoData
    {
        [JsonProperty("account_hash")]
        public string AccountHash { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
