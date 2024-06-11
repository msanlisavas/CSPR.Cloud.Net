using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Deploy
{
    public class DeploysFilterParameters
    {
        [JsonProperty("caller_public_key")]
        public string CallerPublicKey { get; set; }
        [JsonProperty("block_hash")]
        public string BlockHash { get; set; }
        [JsonProperty("contract_package_hash")]
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
