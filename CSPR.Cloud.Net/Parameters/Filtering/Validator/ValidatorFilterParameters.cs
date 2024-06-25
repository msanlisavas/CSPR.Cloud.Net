using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Validator
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class ValidatorFilterParameters
    {
        /// <summary>
        /// Current era validator identifier.
        /// </summary>
        [JsonProperty("era_id")]
        public string EraId { get; set; } // Current era validator identifier

    }
}
