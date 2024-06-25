using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Contract
{
    /// <summary>
    /// The ContractPackage entity provides a normalized representation of the Casper Network Contract Package.
    /// It includes the contract package's metadata based on Contract's type and named keys values and the latest version of the contract type within the package.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract-package">CSPR Cloud API documentation</see>.
    /// </summary>
    public class ContractPackageData
    {
        /// <summary>
        /// Contract package hash represented as a hexadecimal string. Unique contract package identifier.
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        /// <summary>
        /// Public key of the owner of the contract package, represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("owner_public_key")]
        public string OwnerPublicKey { get; set; }

        /// <summary>
        /// Name of the contract package.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the contract package.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Metadata associated with the contract package.
        /// </summary>
        [JsonProperty("metadata")]
        public MetadataData Metadata { get; set; }

        /// <summary>
        /// Contract type identifier of the latest contract version.
        /// </summary>
        [JsonProperty("latest_version_contract_type_id")]
        public int? LatestVersionContractTypeId { get; set; }

        /// <summary>
        /// Timestamp indicating when the contract package was created.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// URL pointing to an icon representing the contract package.
        /// </summary>
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        /// <summary>
        /// Number of deploys associated with the contract package.
        /// </summary>
        [JsonProperty("deploys_number")]
        public int? DeploysNumber { get; set; }
    }
}
