using CSPR.Cloud.Net.Enums;
using CSPR.Cloud.Net.Objects.Contract;
using CSPR.Cloud.Net.Objects.Deploy;
using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Nft
{
    /// <summary>
    /// The NFTTokenAction entity describes standard actions that can be performed on NFTs, such as mint, transfer, burn, etc.
    /// CSPR.cloud supports NFTs compatible with the CEP-47 and CEP-78 Casper Network standards.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token-action">CSPR Cloud API documentation</see>.
    /// </summary>
    public class NFTTokenActionData
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
        public ulong BlockHeight { get; set; }

        /// <summary>
        /// ID describes relation with corresponded NFTToken. Second part of token action identifier.
        /// </summary>
        [JsonProperty("token_tracking_id")]
        public string TokenTrackingId { get; set; }

        /// <summary>
        /// Contract package hash of the NFT contract, represented as a hexadecimal string.
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
        /// NFT action type identifier.
        /// </summary>
        [JsonProperty("nft_action_id")]
        public uint NftActionId { get; set; }

        /// <summary>
        /// Token ID presented on the network.
        /// </summary>
        [JsonProperty("token_id")]
        public string TokenId { get; set; }

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
