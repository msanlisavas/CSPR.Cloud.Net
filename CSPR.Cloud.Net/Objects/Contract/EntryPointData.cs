using CSPR.Cloud.Net.Parameters.General;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CSPR.Cloud.Net.Objects.Contract
{
    /// <summary>
    /// The ContractEntryPoint entity offers a normalized representation of the Casper Network EntryPoint.
    /// Entry points are associated with the Contract entity and describe how to interact with it.
    /// In CSPR.Cloud, only the name of the entry point and its relations to the Contract and ContractPackage are stored.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract-entry-point">CSPR Cloud API documentation</see>.
    /// </summary>
    public class EntryPointData : Paginated
    {
        /// <summary>
        /// Unique identifier of the entry point.
        /// </summary>
        [JsonProperty("id")]
        public int? Id { get; set; }
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
        /// Name of the entry point.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Action type identifier associated with the entry point.
        /// </summary>
        [JsonProperty("action_type_id")]
        public int? ActionTypeId { get; set; }
    }
}
