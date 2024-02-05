using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.AccountInfo
{
    public class AffiliatedAccountData
    {
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
    }
}
