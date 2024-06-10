using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Args
{
    public class ArgsData
    {
        [JsonProperty("amount")]
        public AmountData Amount { get; set; }

        [JsonProperty("id")]
        public IdData Id { get; set; }

        [JsonProperty("target")]
        public TargetData Target { get; set; }
    }
}
