using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Ft
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class FTActionFilterParameters
    {
        /// <summary>
        /// Filters by contract package hash.
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        /// <summary>
        /// Filters by account hash.
        /// </summary>
        [JsonProperty("account_hash")]
        public string AccountHash { get; set; }

        /// <summary>
        /// Filters by the starting block height.
        /// </summary>
        [JsonProperty("from_block_height")]
        public string FromBlockHeight { get; set; }

        /// <summary>
        /// Filters by the ending block height.
        /// </summary>
        [JsonProperty("to_block_height")]
        public string ToBlockHeight { get; set; }

    }
}
