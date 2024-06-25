using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Deploy
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class AccountDeploysFilterParameters
    {
        /// <summary>
        /// Filters by Contract Package Hash
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }
        /// <summary>
        /// Filters by Contract Hash
        /// </summary>
        [JsonProperty("contract_hash")]
        public string ContractHash { get; set; }
        /// <summary>
        /// Filters by Contract EndpointId
        /// </summary>
        [JsonProperty("contract_entrypoint_id")]
        public string ContractEndpointId { get; set; }
        /// <summary>
        /// Filters by From Block Height
        /// </summary>
        [JsonProperty("from_block_height")]
        public string FromBlockHeight { get; set; }
        /// <summary>
        /// Filters by To Block Height
        /// </summary>
        [JsonProperty("to_block_height")]
        public string ToBlockHeight { get; set; }
    }
}
