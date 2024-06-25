using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Contract
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class ContractsFilterParameters
    {

        /// <summary>
        /// Filters by Deploy Hash
        /// </summary>
        [JsonProperty("deploy_hash")]
        public string DeployHash { get; set; }
        /// <summary>
        /// Filters by Contract Package Hash
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }
        /// <summary>
        /// Filters by From Block Height
        /// </summary>
        [JsonProperty("from_block_height")]
        public ulong? FromBlockHeight { get; set; }
        /// <summary>
        /// Filters by To Block Height
        /// </summary>
        [JsonProperty("to_block_height")]
        public ulong? ToBlockHeight { get; set; }
    }
}
