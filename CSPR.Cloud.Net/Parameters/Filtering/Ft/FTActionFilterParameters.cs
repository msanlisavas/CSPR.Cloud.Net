using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Ft
{
    public class FTActionFilterParameters
    {
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }
        [JsonProperty("account_hash")]
        public string AccountHash { get; set; }
        [JsonProperty("from_block_height")]
        public string FromBlockHeight { get; set; }
        [JsonProperty("to_block_height")]
        public string ToBlockHeight { get; set; }
    }
}
