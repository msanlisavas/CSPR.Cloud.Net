using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Deploy
{
    public class BlockDeploysFilterParameters
    {
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }
        [JsonProperty("contract_hash")]
        public string ContractHash { get; set; }
        [JsonProperty("contract_entrypoint_id")]
        public string ContractEndpointId { get; set; }
        [JsonProperty("caller_public_key")]
        public string CallerPublicKey { get; set; }
    }
}
