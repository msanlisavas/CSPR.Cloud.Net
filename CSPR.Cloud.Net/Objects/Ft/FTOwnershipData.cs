using CSPR.Cloud.Net.Objects.Contract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Ft
{
    /// <summary>
    /// The FTOwnership entity represents a fungible token ownership relation between accounts and contract packages, as well as provides the corresponding token balances.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/fungible-token-ownership">CSPR Cloud API documentation</see>.
    /// </summary>
    public class FTOwnershipData
    {
        /// <summary>
        /// Fungible tokens balance in the network.
        /// </summary>
        [JsonProperty("balance")]
        public string Balance { get; set; }

        /// <summary>
        /// Fungible contract package hash represented as a hexadecimal string. Second part of token ownership identifier.
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        /// <summary>
        /// Owner hash represented as a hexadecimal string. First part of token ownership identifier.
        /// </summary>
        [JsonProperty("owner_hash")]
        public string OwnerHash { get; set; }

        /// <summary>
        /// Data about the contract package.
        /// </summary>
        [JsonProperty("contract_package")]
        public ContractPackageData ContractPackage { get; set; }
    }

}
