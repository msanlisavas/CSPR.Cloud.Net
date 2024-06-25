using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Deploy
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class DeploysFilterParameters
    {
        /// <summary>
        /// Filters by the caller's public key.
        /// </summary>
        [JsonProperty("caller_public_key")]
        public string CallerPublicKey { get; set; }

        /// <summary>
        /// Filters by block hash.
        /// </summary>
        [JsonProperty("block_hash")]
        public string BlockHash { get; set; }

        /// <summary>
        /// Filters by contract package hash.
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        /// <summary>
        /// Filters by contract hash.
        /// </summary>
        [JsonProperty("contract_hash")]
        public string ContractHash { get; set; }

        /// <summary>
        /// Filters by contract entry point ID.
        /// </summary>
        [JsonProperty("contract_entrypoint_id")]
        public string ContractEndpointId { get; set; }

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
