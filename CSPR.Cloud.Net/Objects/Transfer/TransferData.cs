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
    }
}
