using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Contract
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class ByContractPackageFilterParameters
    {
        /// <summary>
        /// Filters by Deploy Hash
        /// </summary>
        [JsonProperty("deploy_hash")]
        public string DeployHash { get; set; }
    }
}
