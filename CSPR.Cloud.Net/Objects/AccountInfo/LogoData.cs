using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.AccountInfo
{
    public class LogoData
    {
        [JsonProperty("png_1024")]
        public string Png1024 { get; set; }

        [JsonProperty("png_256")]
        public string Png256 { get; set; }

        [JsonProperty("svg")]
        public string Svg { get; set; }
    }
}
