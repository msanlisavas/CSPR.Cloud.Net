using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Ft
{
    public class FTDailyDexRateData
    {
        [JsonProperty("token_contract_package_hash")]
        public string TokenContractPackageHash { get; set; }

        [JsonProperty("target_token_contract_package_hash")]
        public string TargetTokenContractPackageHash { get; set; }

        [JsonProperty("amount")]
        public double? Amount { get; set; }

        [JsonProperty("volume")]
        public string Volume { get; set; }

        [JsonProperty("dex_id")]
        public int? DexId { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("ft_daily_rate")]
        public float? FtDailyRate { get; set; }
    }
}
