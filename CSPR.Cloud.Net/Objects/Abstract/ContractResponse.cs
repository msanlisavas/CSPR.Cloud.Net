using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Abstract
{
    public class ContractResponse<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }
        [JsonProperty("latest_version_contract_type_id")]
        public int LatestVersionContractTypeId { get; set; }
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }
        [JsonProperty("deploys_number")]
        public int DeploysNumber { get; set; }
    }
}
