using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Contract
{
    public class ContractPackageData
    {
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        [JsonProperty("owner_public_key")]
        public string OwnerPublicKey { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("metadata")]
        public MetadataData Metadata { get; set; }

        [JsonProperty("latest_version_contract_type_id")]
        public int? LatestVersionContractTypeId { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonProperty("deploys_number")]
        public int? DeploysNumber { get; set; }
    }
}
