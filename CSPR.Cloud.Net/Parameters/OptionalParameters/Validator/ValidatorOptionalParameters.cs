using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Validator
{
    /// <summary>
    /// Represents optional parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/optional-properties">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class ValidatorOptionalParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include account info. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("account_info")]
        public bool AccountInfo { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include centralized account info. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("centralized_account_info")]
        public bool CentralizedAccountInfo { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include average performance. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("average_performance")]
        public bool AveragePerformance { get; set; } = false;

    }
}
