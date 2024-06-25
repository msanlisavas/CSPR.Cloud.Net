using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Supply
{
    public class SupplyData
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("total")]
        public ulong? Total { get; set; }
        [JsonProperty("circulating")]
        public ulong? Circulating { get; set; }
        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }
    }
}
