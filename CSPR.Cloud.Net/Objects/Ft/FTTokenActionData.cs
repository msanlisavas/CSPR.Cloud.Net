using CSPR.Cloud.Net.Enums;
using CSPR.Cloud.Net.Objects.Contract;
using CSPR.Cloud.Net.Objects.Deploy;
using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Ft
{
    public class FTTokenActionData
    {
        [JsonProperty("deploy_hash")]
        public string DeployHash { get; set; }

        [JsonProperty("block_height")]
        public ulong? BlockHeight { get; set; }

        [JsonProperty("transform_idx")]
        public int? TransformIdx { get; set; }

        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        [JsonProperty("from_hash")]
        public string FromHash { get; set; }

        [JsonProperty("from_type")]
        public HashType? FromType { get; set; }

        [JsonProperty("to_hash")]
        public string ToHash { get; set; }

        [JsonProperty("to_type")]
        public HashType? ToType { get; set; }

        [JsonProperty("ft_action_type_id")]
        public uint? FtActionTypeId { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }
        [JsonProperty("contract_package")]
        public ContractPackageData ContractPackage { get; set; }
        [JsonProperty("deploy")]
        public DeployData Deploy { get; set; }
        [JsonProperty("from_public_key")]
        public string FromPublicKey { get; set; }
        [JsonProperty("to_public_key")]
        public string ToPublicKey { get; set; }
    }
}
