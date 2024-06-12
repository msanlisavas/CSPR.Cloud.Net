using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Contract
{
    public class ContractData
    {
        [JsonProperty("contract_hash")]
        public string ContractHash { get; set; }

        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        [JsonProperty("deploy_hash")]
        public string DeployHash { get; set; }

        [JsonProperty("block_height")]
        public ulong BlockHeight { get; set; }

        [JsonProperty("contract_type_id")]
        public int? ContractTypeId { get; set; }

        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }

        [JsonProperty("contract_version")]
        public int? ContractVersion { get; set; }

        [JsonProperty("is_disabled")]
        public bool IsDisabled { get; set; }
        [JsonProperty("contract_package")]
        public ContractPackageData ContractPackage { get; set; }
    }
}
