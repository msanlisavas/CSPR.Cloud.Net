using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CSPR.Cloud.Net.Objects.Socket
{
    /// <summary>
    /// Custom contract-level event emitted during a deploy execution.
    /// For more information, see <see href="https://docs.cspr.cloud/streaming-api/contract-level-events">CSPR Cloud Streaming API documentation</see>.
    /// </summary>
    public class ContractEventStreamData
    {
        /// <summary>
        /// Contract package hash, 64-character hexadecimal string.
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        /// <summary>
        /// Contract hash, 64-character hexadecimal string. Set only for CES events.
        /// </summary>
        [JsonProperty("contract_hash")]
        public string ContractHash { get; set; }

        /// <summary>
        /// Event name / identifier emitted by the contract.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Structured event payload. Shape is contract-defined, exposed as <see cref="JObject"/>.
        /// </summary>
        [JsonProperty("data")]
        public JObject Data { get; set; }

        /// <summary>
        /// Hexadecimal-encoded raw event bytes. Populated only when the subscription included
        /// the <c>includes=raw_data</c> option.
        /// </summary>
        [JsonProperty("raw_data")]
        public string RawData { get; set; }
    }
}
