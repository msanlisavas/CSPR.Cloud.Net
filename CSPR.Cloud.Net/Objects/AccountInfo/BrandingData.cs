using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.AccountInfo
{
    public class BrandingData
    {
        [JsonProperty("logo")]
        public LogoData Logo { get; set; }
    }
}
