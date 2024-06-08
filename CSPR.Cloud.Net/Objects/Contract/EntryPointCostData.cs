using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Contract
{
    public class EntryPointCostData
    {
        [JsonProperty("deploys_num")]
        public int? DeploysNum { get; set; }

        [JsonProperty("since")]
        public DateTime? Since { get; set; }

        [JsonProperty("avg_cost")]
        public decimal? AvgCost { get; set; }

        [JsonProperty("min_cost")]
        public decimal? MinCost { get; set; }

        [JsonProperty("max_cost")]
        public decimal? MaxCost { get; set; }

        [JsonProperty("avg_payment_amount")]
        public decimal? AvgPaymentAmount { get; set; }

        [JsonProperty("min_payment_amount")]
        public decimal? MinPaymentAmount { get; set; }

        [JsonProperty("max_payment_amount")]
        public decimal? MaxPaymentAmount { get; set; }
    }
}
