using CSPR.Cloud.Net.Enums;
using CSPR.Cloud.Net.Objects.Contract;
using CSPR.Cloud.Net.Objects.Deploy;
using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Ft
{
    /// <summary>
    /// The FTTokenAction entity describes standard actions that can be performed on fungible tokens, such as mint, transfer, or burn.
    /// CSPR.cloud supports fungible tokens compatible with the CEP-18 Casper Network standard.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/fungible-token-action">CSPR Cloud API documentation</see>.
    /// </summary>
    public class FTTokenActionData
    {
        /// <summary>
        /// Deploy hash in which the token action occurred. First part of token action identifier, represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("deploy_hash")]
        public string DeployHash { get; set; }

        /// <summary>
        /// Height of the block in which the token action occurred.
        /// </summary>
        [JsonProperty("block_height")]
        public ulong? BlockHeight { get; set; }

        /// <summary>
        /// ID of the transform in the deploy execution result containing performed token action in deploy execution. Second part of token action identifier.
        /// </summary>
        [JsonProperty("transform_idx")]
        public int? TransformIdx { get; set; }

        /// <summary>
        /// Contract package hash of fungible token contract, represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        /// <summary>
        /// Action source account or contract hash, represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("from_hash")]
        public string FromHash { get; set; }

        /// <summary>
        /// Action source hash type: 0 for account, 1 for contract.
        /// </summary>
        [JsonProperty("from_type")]
        public HashType? FromType { get; set; }

        /// <summary>
        /// Action target account or contract hash, represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("to_hash")]
        public string ToHash { get; set; }

        /// <summary>
        /// Action target hash type: 0 for account, 1 for contract.
        /// </summary>
        [JsonProperty("to_type")]
        public HashType? ToType { get; set; }

        /// <summary>
        /// Fungible token action type identifier.
        /// </summary>
        [JsonProperty("ft_action_type_id")]
        public uint? FtActionTypeId { get; set; }

        /// <summary>
        /// Tokens amount value used during the token action.
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// The timestamp of performed token action.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// Data about the contract package.
        /// </summary>
        [JsonProperty("contract_package")]
        public ContractPackageData ContractPackage { get; set; }

        /// <summary>
        /// Data about the deploy.
        /// </summary>
        [JsonProperty("deploy")]
        public DeployData Deploy { get; set; }

        /// <summary>
        /// Public key of the action source account represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("from_public_key")]
        public string FromPublicKey { get; set; }

        /// <summary>
        /// Public key of the action target account represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("to_public_key")]
        public string ToPublicKey { get; set; }
    }
}
