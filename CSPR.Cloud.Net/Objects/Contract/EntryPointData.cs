using CSPR.Cloud.Net.Parameters.General;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CSPR.Cloud.Net.Objects.Contract
{
    public class EntryPointData : Paginated
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        [JsonProperty("contract_hash")]
        public string ContractHash { get; set; }
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("action_type_id")]
        public int? ActionTypeId { get; set; }
    }
}
