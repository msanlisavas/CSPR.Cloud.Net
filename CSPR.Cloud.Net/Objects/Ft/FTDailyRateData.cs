using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Objects.Ft
{
    public class FTDailyRateData
    {
        [JsonProperty("token_contract_package_hash")]
        public string TokenContractPackageHash { get; set; }

        [JsonProperty("currency_id")]
        public int? CurrencyId { get; set; }

        [JsonProperty("amount")]
        public double? Amount { get; set; }

        [JsonProperty("volume")]
        public string Volume { get; set; }

        [JsonProperty("dex_ids")]
        public List<int> DexIds { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }
}
