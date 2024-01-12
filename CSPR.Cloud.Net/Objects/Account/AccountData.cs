using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Account
{
    public class AccountData
    {
        [JsonProperty("account_hash")]
        public string AccountHash { get; set; }

        [JsonProperty("balance")]
        public long Balance { get; set; }

        [JsonProperty("main_purse_uref")]
        public string MainPurseUref { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
    }
}
