using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Contract
{
    public class ContractsFilterParameters
    {

        [JsonProperty("deploy_hash")]
        public string DeployHash { get; set; }
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }
        [JsonProperty("from_block_height")]
        public ulong? FromBlockHeight { get; set; }
        [JsonProperty("to_block_height")]
        public ulong? ToBlockHeight { get; set; }
    }
}
