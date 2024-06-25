using CSPR.Cloud.Net.Objects.AccountInfo;
using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Block
{
    /// <summary>
    /// The Block entity provides a normalized representation of the Casper Network block.
    /// It doesn't contain the executed deploy hashes as the network block object. However, they could be queried separately using the Deploy API.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/block">CSPR Cloud API documentation</see>.
    /// </summary>
    public class BlockData
    {
        /// <summary>
        /// Block height. Primary block identifier.
        /// </summary>
        [JsonProperty("block_height")]
        public ulong? BlockHeight { get; set; }

        /// <summary>
        /// Block hash represented as a hexadecimal string. Secondary block identifier.
        /// </summary>
        [JsonProperty("block_hash")]
        public string BlockHash { get; set; }

        /// <summary>
        /// Parent block hash represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("parent_block_hash")]
        public string ParentBlockHash { get; set; }

        /// <summary>
        /// Identifier of the network's state after executing the block's deploys represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("state_root_hash")]
        public string StateRootHash { get; set; }

        /// <summary>
        /// Era ID in which the block was created.
        /// </summary>
        [JsonProperty("era_id")]
        public uint? EraId { get; set; }

        /// <summary>
        /// Public key of the validator who proposed the block represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("proposer_public_key")]
        public string ProposerPublicKey { get; set; }

        /// <summary>
        /// Number of native transfer deploys included in the block.
        /// </summary>
        [JsonProperty("native_transfers_number")]
        public ushort? NativeTransfersNumber { get; set; }

        /// <summary>
        /// Number of contract calls included in the block.
        /// </summary>
        [JsonProperty("contract_calls_number")]
        public ushort? ContractCallsNumber { get; set; }

        /// <summary>
        /// true if the block is the last one in the era.
        /// </summary>
        [JsonProperty("is_switch_block")]
        public bool IsSwitchBlock { get; set; }

        /// <summary>
        /// The timestamp from when the block was proposed.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// Information about the proposer account.
        /// </summary>
        [JsonProperty("proposer_account_info")]
        public AccountInfoData ProposerAccountInfo { get; set; }
    }

}
