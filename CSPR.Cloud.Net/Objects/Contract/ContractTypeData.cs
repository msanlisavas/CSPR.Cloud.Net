using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Contract
{
    public class ContractTypeData
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
