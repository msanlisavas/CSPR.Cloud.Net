using CSPR.Cloud.Net.Objects.Args;
using CSPR.Cloud.Net.Objects.Contract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Nft
{
    public class NFTTokenData
    {
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; } // NFT contract package hash represented as a hexadecimal string. First part of token identifier

        [JsonProperty("block_height")]
        public ulong BlockHeight { get; set; } // Height of the block in which the token was minted.

        [JsonProperty("onchain_metadata")]
        public ArgsData OnchainMetadata { get; set; } // On-chain NFT metadata

        [JsonProperty("offchain_metadata")]
        public ArgsData OffchainMetadata { get; set; } // Off-chain NFT metadata

        [JsonProperty("offchain_metadata_status")]
        public uint OffchainMetadataStatus { get; set; } // NFT token offchain metadata status identifier

        [JsonProperty("is_burned")]
        public bool IsBurned { get; set; } // true if the token was burned

        [JsonProperty("contract_package")]
        public ContractPackageData ContractPackage { get; set; } // NFT contract package data
        [JsonProperty("owner_public_key")]
        public string OwnerPublicKey { get; set; } // Owner public key if it's an account
    }
}
