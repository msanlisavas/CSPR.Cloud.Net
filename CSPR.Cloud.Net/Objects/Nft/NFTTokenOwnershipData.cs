using CSPR.Cloud.Net.Enums;
using CSPR.Cloud.Net.Objects.Contract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Nft
{
    /// <summary>
    /// The NFTTokenOwnership entity represents a NFT ownership relation between accounts and contract packages, as well as provides the total tokens number.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token-ownership">CSPR Cloud API documentation</see>.
    /// </summary>
    public class NFTTokenOwnershipData
    {
        /// <summary>
        /// Owner hash represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("owner_hash")]
        public string OwnerHash { get; set; }

        /// <summary>
        /// NFT contract package hash represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        /// <summary>
        /// NFT token owner type: 0 for account, 1 for contract.
        /// </summary>
        [JsonProperty("owner_type")]
        public OwnerType? OwnerType { get; set; }

        /// <summary>
        /// Number of NFTs owned by the owner account.
        /// </summary>
        [JsonProperty("tokens_number")]
        public uint? TokensNumber { get; set; }

        /// <summary>
        /// Data about the contract package.
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
