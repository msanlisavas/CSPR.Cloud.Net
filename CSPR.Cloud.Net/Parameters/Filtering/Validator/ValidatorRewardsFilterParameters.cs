using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Validator
{
    /// <summary>
    /// Filtering parameters for validator rewards endpoints (<c>/validators/{pk}/rewards</c> and <c>/validators/{pk}/era-rewards</c>).
    /// Supports era-range filtering (v2.4.0+).
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class ValidatorRewardsFilterParameters
    {
        /// <summary>
        /// Restricts the result set to eras at or after the given era id.
        /// </summary>
        [JsonProperty("from_era_id")]
        public string FromEraId { get; set; }

        /// <summary>
        /// Restricts the result set to eras at or before the given era id.
        /// </summary>
        [JsonProperty("to_era_id")]
        public string ToEraId { get; set; }
    }
}
