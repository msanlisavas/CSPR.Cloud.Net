using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Validator
{
    /// <summary>
    /// Base Timestamp Sorting Parameter
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/sorting">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class ValidatorsHistoricalAveragePerformanceSortingParameters : BaseSortingParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether to order by era ID. Set it to true to sort by era ID.
        /// </summary>
        [JsonProperty("era_id")]
        public bool OrderByEraId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to sort performances by validator public key. Set it to true to sort by validator public key.
        /// </summary>
        [JsonProperty("public_key")]
        public bool OrderByPublicKeyPerformance { get; set; } // Sort performances by validator public key

    }
}
