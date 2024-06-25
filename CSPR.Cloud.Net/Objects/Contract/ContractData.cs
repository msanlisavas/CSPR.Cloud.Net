using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Contract
{
    /// <summary>
    /// The Contract entity offers a normalized representation of the Casper Network Contract without including entry point hashes, which can be queried separately using the Contract entry point API.
    /// Additionally, this entity omits the contract's named keys but utilizes them for identifying the contract's type.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract">CSPR Cloud API documentation</see>.
    /// </summary>
    public class ContractData
    {
        /// <summary>
        /// Contract hash represented as a hexadecimal string. Unique contract identifier.
        /// </summary>
        [JsonProperty("contract_hash")]
        public string ContractHash { get; set; }

        /// <summary>
        /// Hash of the contract package, this contract version is a part of, represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        /// <summary>
        /// Hash of the deploy that deployed the contract to the network represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("deploy_hash")]
        public string DeployHash { get; set; }

        /// <summary>
        /// Height of the block in which the contract was deployed to the network.
        /// </summary>
        [JsonProperty("block_height")]
        public ulong BlockHeight { get; set; }

        /// <summary>
        /// Identifier representing the type of the contract, check the types list here.
        /// </summary>
        [JsonProperty("contract_type_id")]
        public int? ContractTypeId { get; set; }

        /// <summary>
        /// Timestamp indicating when the contract was created.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// Version number associated with the contract.
        /// </summary>
        [JsonProperty("contract_version")]
        public int? ContractVersion { get; set; }

        /// <summary>
        /// Indicates whether the contract is currently disabled.
        /// </summary>
        [JsonProperty("is_disabled")]
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Data about the contract package.
        /// </summary>
        [JsonProperty("contract_package")]
        public ContractPackageData ContractPackage { get; set; }
    }
}
