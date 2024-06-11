using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Deploy
{
    public class AccountDeploysFilterParameters
    {
        public string ContractPackageHash { get; set; }
        [JsonProperty("contract_hash")]
        public string ContractHash { get; set; }
        [JsonProperty("contract_entrypoint_id")]
        public string ContractEndpointId { get; set; }
        [JsonProperty("from_block_height")]
        public string FromBlockHeight { get; set; }
        [JsonProperty("to_block_height")]
        public string ToBlockHeight { get; set; }
    }
}
