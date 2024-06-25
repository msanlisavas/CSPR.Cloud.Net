using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Transfer
{
    public class TransferData
    {
        [JsonProperty("id")]
        public ulong? Id { get; set; }

        [JsonProperty("deploy_hash")]
        public string DeployHash { get; set; }

        [JsonProperty("block_height")]
        public ulong? BlockHeight { get; set; }

        [JsonProperty("transform_key")]
        public string TransformKey { get; set; }

        [JsonProperty("initiator_account_hash")]
        public string InitiatorAccountHash { get; set; }

        [JsonProperty("from_purse")]
        public string FromPurse { get; set; }

        [JsonProperty("to_purse")]
        public string ToPurse { get; set; }

        [JsonProperty("to_account_hash")]
        public string ToAccountHash { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }
        [JsonProperty("initiator_public_key")]
        public string InitiatorPublicKey { get; set; }
        [JsonProperty("to_public_key")]
        public string ToPublicKey { get; set; }
        [JsonProperty("from_purse_public_key")]
        public string FromPursePublicKey { get; set; }
        [JsonProperty("to_purse_public_key")]
        public string ToPursePublicKey { get; set; }
        [JsonProperty("to_account_info")]
        public AccountInfoData ToAccountInfo { get; set; }
        [JsonProperty("to_centralized_account_info")]
        public CentralizedAccountInfoData ToCentralizedAccountInfo { get; set; }
        [JsonProperty("from_purse_account_info")]
        public AccountInfoData FromPurseAccountInfo { get; set; }
        [JsonProperty("from_purse_centralized_account_info")]
        public CentralizedAccountInfoData FromPurseCentralizedAccountInfo { get; set; }
        [JsonProperty("to_purse_account_info")]
        public AccountInfoData ToPurseAccountInfo { get; set; }
        [JsonProperty("to_purse_centralized_account_info")]
        public CentralizedAccountInfoData ToPurseCentralizedAccountInfo { get; set; }
        [JsonProperty("rate")]
        public float? Rate { get; set; }

    }
}
