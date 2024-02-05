using CSPR.Cloud.Net.Objects.AccountInfo;
using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Block
{
    public class BlockData
    {
        [JsonProperty("block_height")]
        public ulong? BlockHeight { get; set; }

        [JsonProperty("block_hash")]
        public string BlockHash { get; set; }

        [JsonProperty("parent_block_hash")]
        public string ParentBlockHash { get; set; }

        [JsonProperty("state_root_hash")]
        public string StateRootHash { get; set; }

        [JsonProperty("era_id")]
        public uint? EraId { get; set; }

        [JsonProperty("proposer_public_key")]
        public string ProposerPublicKey { get; set; }

        [JsonProperty("native_transfers_number")]
        public ushort? NativeTransfersNumber { get; set; }

        [JsonProperty("contract_calls_number")]
        public ushort? ContractCallsNumber { get; set; }

        [JsonProperty("is_switch_block")]
        public bool IsSwitchBlock { get; set; }

        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }
        [JsonProperty("proposer_account_info")]
        public AccountInfoData ProposerAccountInfo { get; set; }
    }
}
