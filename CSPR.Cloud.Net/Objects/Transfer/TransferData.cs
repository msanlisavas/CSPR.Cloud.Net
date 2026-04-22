using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Transfer
{
    /// <summary>
    /// The Transfer entity represents a successful native token (CSPR) transfer on the Casper network.
    /// Transfers are tracked from the WriteTransfer transforms found in the deploy execution results.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/transfer">CSPR Cloud API documentation</see>.
    /// </summary>
    public class TransferData
    {
        /// <summary>
        /// Transfer identifier provided by the deploy caller if the deploy is a native transfer. Default is 0.
        /// </summary>
        [JsonProperty("id")]
        public ulong? Id { get; set; }

        /// <summary>
        /// Deploy hash represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("deploy_hash")]
        public string DeployHash { get; set; }

        /// <summary>
        /// Height of the block in which the transfer happened.
        /// </summary>
        [JsonProperty("block_height")]
        public ulong? BlockHeight { get; set; }

        /// <summary>
        /// WriteTransfer transform key represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("transform_key")]
        public string TransformKey { get; set; }

        /// <summary>
        /// Deploy caller account hash represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("initiator_account_hash")]
        public string InitiatorAccountHash { get; set; }

        /// <summary>
        /// Source purse URef in the uref-dead...beef-007 format.
        /// </summary>
        [JsonProperty("from_purse")]
        public string FromPurse { get; set; }

        /// <summary>
        /// Target purse URef in the uref-dead...beef-007 format.
        /// </summary>
        [JsonProperty("to_purse")]
        public string ToPurse { get; set; }

        /// <summary>
        /// Transfer recipient account hash represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("to_account_hash")]
        public string ToAccountHash { get; set; }

        /// <summary>
        /// Transfer amount in motes. The type is string to avoid overflow in languages that don't support uint64, which is the correct type.
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Deploy creation timestamp in the ISO 8601 format.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// Public key of the deploy caller represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("initiator_public_key")]
        public string InitiatorPublicKey { get; set; }

        /// <summary>
        /// Public key of the transfer recipient represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("to_public_key")]
        public string ToPublicKey { get; set; }

        /// <summary>
        /// Public key of the account that owns the transfer source purse represented as a hexadecimal string. Null when the sender purse is not owned by an account.
        /// </summary>
        [JsonProperty("from_purse_public_key")]
        public string FromPursePublicKey { get; set; }

        /// <summary>
        /// Public key of the account that owns the transfer target purse represented as a hexadecimal string. Null when the target purse is not owned by an account.
        /// </summary>
        [JsonProperty("to_purse_public_key")]
        public string ToPursePublicKey { get; set; }

        /// <summary>
        /// Transfer recipient account info.
        /// </summary>
        [JsonProperty("to_account_info")]
        public AccountInfoData ToAccountInfo { get; set; }

        /// <summary>
        /// Transfer recipient centralized account info.
        /// </summary>
        [JsonProperty("to_centralized_account_info")]
        public CentralizedAccountInfoData ToCentralizedAccountInfo { get; set; }

        /// <summary>
        /// Account info of the account that owns the transfer source purse. Null when the sender purse is not owned by an account.
        /// </summary>
        [JsonProperty("from_purse_account_info")]
        public AccountInfoData FromPurseAccountInfo { get; set; }

        /// <summary>
        /// Centralized account info of the account that owns the transfer source purse. Null when the sender purse is not owned by an account.
        /// </summary>
        [JsonProperty("from_purse_centralized_account_info")]
        public CentralizedAccountInfoData FromPurseCentralizedAccountInfo { get; set; }

        /// <summary>
        /// Account info of the account that owns the transfer target purse. Null when the target purse is not owned by an account.
        /// </summary>
        [JsonProperty("to_purse_account_info")]
        public AccountInfoData ToPurseAccountInfo { get; set; }

        /// <summary>
        /// Centralized account info of the account that owns the transfer target purse. Null when the target purse is not owned by an account.
        /// </summary>
        [JsonProperty("to_purse_centralized_account_info")]
        public CentralizedAccountInfoData ToPurseCentralizedAccountInfo { get; set; }

        /// <summary>
        /// Rate of the transfer.
        /// </summary>
        [JsonProperty("rate")]
        public float? Rate { get; set; }

        /// <summary>
        /// Monotonic transfer index assigned by the indexer — distinct from <see cref="Id"/>, which is caller-supplied.
        /// </summary>
        [JsonProperty("transfer_index")]
        public ulong? TransferIndex { get; set; }

        /// <summary>
        /// The transfer initiator's registered CSPR.name when the <c>initiator_cspr_name</c> includer is requested (v2.1.0+).
        /// </summary>
        [JsonProperty("initiator_cspr_name")]
        public string InitiatorCsprName { get; set; }

        /// <summary>
        /// The recipient's registered CSPR.name when the <c>to_cspr_name</c> includer is requested (v2.1.0+).
        /// </summary>
        [JsonProperty("to_cspr_name")]
        public string ToCsprName { get; set; }

        /// <summary>
        /// CSPR.name of the account that owns the transfer source purse (v2.1.0+).
        /// </summary>
        [JsonProperty("from_purse_cspr_name")]
        public string FromPurseCsprName { get; set; }

        /// <summary>
        /// CSPR.name of the account that owns the transfer target purse (v2.1.0+).
        /// </summary>
        [JsonProperty("to_purse_cspr_name")]
        public string ToPurseCsprName { get; set; }
    }

}
