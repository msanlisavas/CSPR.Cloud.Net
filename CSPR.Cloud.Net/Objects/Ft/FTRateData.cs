using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Ft
{
    public class FTRateData
    {
        [JsonProperty("token_contract_package_hash")]
        public string TokenContractPackageHash { get; set; }

        [JsonProperty("currency_id")]
        public int? CurrencyId { get; set; }

        [JsonProperty("amount")]
        public double? Amount { get; set; }

        [JsonProperty("volume")]
        public string Volume { get; set; }

        [JsonProperty("dex_id")]
        public int? DexId { get; set; }

        [JsonProperty("transaction_hash")]
        public string TransactionHash { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}
