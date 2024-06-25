using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Filtering.Validator
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class ValidatorsHistoricalAveragePerformanceFilterParameters
    {
        /// <summary>
        /// List of era identifiers.
        /// </summary>
        [JsonProperty("era_id")]
        public List<string> EraIds { get; set; }

        /// <summary>
        /// List of public keys.
        /// </summary>
        [JsonProperty("public_key")]
        public List<string> PublicKeys { get; set; }

    }
}
