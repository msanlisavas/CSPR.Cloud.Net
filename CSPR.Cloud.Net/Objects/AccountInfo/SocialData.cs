using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.AccountInfo
{
    public class SocialData
    {
        [JsonProperty("facebook")]
        public string Facebook { get; set; }

        [JsonProperty("github")]
        public string Github { get; set; }

        [JsonProperty("keybase")]
        public string Keybase { get; set; }

        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("reddit")]
        public string Reddit { get; set; }

        [JsonProperty("telegram")]
        public string Telegram { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        [JsonProperty("wechat")]
        public string Wechat { get; set; }

        [JsonProperty("youtube")]
        public string Youtube { get; set; }
    }
}
