using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Ft
{
    public class FTDexRateFilterParameters
    {
        [JsonProperty("target_contract_package_hash")]
        public string TargetContractPackageHash { get; set; }

        [JsonProperty("dex_id")]
        public string DexId { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }
}
