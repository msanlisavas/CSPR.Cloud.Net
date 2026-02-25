using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Ft
{
    public class FTRateFilterParameters
    {
        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("dex_id")]
        public string DexId { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }
}
