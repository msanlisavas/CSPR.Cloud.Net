using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Rate
{
    public class CurrencyData
    {
        [JsonProperty("id")]
        public uint? Id { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("type_id")]
        public uint? TypeId { get; set; }
    }
}
