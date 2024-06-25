using CSPR.Cloud.Net.Enums;
using CSPR.Cloud.Net.Objects.Args;
using CSPR.Cloud.Net.Objects.Contract;
using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Nft
{
    /// <summary>
    /// The NFTToken entity provides representation of an NFT on Casper Network.
    /// CSPR.cloud supports NFTs compatible with the CEP-47 and CEP-78 Casper Network standards.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token">CSPR Cloud API documentation</see>.
    /// </summary>
    public class NFTTokenData
    {
        /// <summary>
        /// NFT contract package hash represented as a hexadecimal string. First part of token identifier.
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        /// <summary>
        /// Height of the block in which the token was minted.
        /// </summary>
        [JsonProperty("block_height")]
        public ulong BlockHeight { get; set; }

        /// <summary>
        /// On-chain NFT metadata.
        /// </summary>
        [JsonProperty("onchain_metadata")]
        public ArgsData OnchainMetadata { get; set; }

        /// <summary>
        /// Off-chain NFT metadata.
        /// </summary>
        [JsonProperty("offchain_metadata")]
        public ArgsData OffchainMetadata { get; set; }

        /// <summary>
        /// NFT token offchain metadata status identifier.
        /// </summary>
        [JsonProperty("offchain_metadata_status")]
        public uint OffchainMetadataStatus { get; set; }

        /// <summary>
        /// true if the token was burned.
        /// </summary>
        [JsonProperty("is_burned")]
        public bool IsBurned { get; set; }

        /// <summary>
        /// NFT token owner account or contract hash, represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("owner_hash")]
        public string OwnerHash { get; set; }

        /// <summary>
        /// NFT owner type. 0 for account, 1 for contract.
        /// </summary>
        [JsonProperty("owner_type")]
        public OwnerType OwnerType { get; set; }

        /// <summary>
        /// The timestamp of token creation.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// Token ID under the contract. Second part of token identifier.
        /// </summary>
        [JsonProperty("token_id")]
        public string TokenId { get; set; }

        /// <summary>
        /// NFT standard identifier.
        /// </summary>
        [JsonProperty("token_standard_id")]
        public byte TokenStandardId { get; set; }

        /// <summary>
        /// NFT token tracking identifier.
        /// </summary>
        [JsonProperty("tracking_id")]
        public string TrackingId { get; set; }

        /// <summary>
        /// NFT contract package data.
        /// </summary>
        [JsonProperty("contract_package")]
        public ContractPackageData ContractPackage { get; set; }

        /// <summary>
        /// Owner public key if it's an account.
        /// </summary>
        [JsonProperty("owner_public_key")]
        public string OwnerPublicKey { get; set; }
    }
}
