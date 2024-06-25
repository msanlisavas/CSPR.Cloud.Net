using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Filtering.Validator
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class ValidatorsFilterParameters
    {

        /// <summary>
        /// Current era identifier.
        /// </summary>
        [JsonProperty("era_id")]
        public string EraId { get; set; } // Current era id

        /// <summary>
        /// Indicates whether the validator is active.
        /// </summary>
        [JsonProperty("is_active")]
        public bool? IsActive { get; set; }

        /// <summary>
        /// List of public keys.
        /// </summary>
        [JsonProperty("public_key")]
        public List<string> PublicKeys { get; set; }
    }
}
