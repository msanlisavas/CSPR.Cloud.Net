using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Rate
{
    public class RateData
    {
        [JsonProperty("currency_id")]
        public byte CurrencyId { get; set; }
        [JsonProperty("amount")]
        public float? Amount { get; set; }
        [JsonProperty("created")]
        public DateTime? Created { get; set; }
    }
}
