using CSPR.Cloud.Net.Objects.Args;
using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Deploy
{
    public class DeployData
    {
        [JsonProperty("deploy_hash")]
        public string DeployHash { get; set; }

        [JsonProperty("block_hash")]
        public string BlockHash { get; set; }

        [JsonProperty("block_height")]
        public ulong? BlockHeight { get; set; }

        [JsonProperty("caller_public_key")]
        public string CallerPublicKey { get; set; }

        [JsonProperty("execution_type_id")]
        public byte? ExecutionTypeId { get; set; }

        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        [JsonProperty("contract_hash")]
        public string ContractHash { get; set; }

        [JsonProperty("entry_point_id")]
        public uint? EntryPointId { get; set; }

        [JsonProperty("args")]
        public ArgsData Args { get; set; }

        [JsonProperty("payment_amount")]
        public string PaymentAmount { get; set; }

        [JsonProperty("cost")]
        public string Cost { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }
    }
}
