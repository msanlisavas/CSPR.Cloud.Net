using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Validator
{
    /// <summary>
    /// The ValidatorPerformance entity provides the validator performance score as the percentage of rewards received per one token relative to the maximum ratio received by a validator in the given era.
    /// A validator that received the highest ratio will have a 100 score.
    /// The average performance endpoints return moving average of the validator performance for the past 360 eras (approximately one month).
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator-performance">CSPR Cloud API documentation</see>.
    /// </summary>
    public class RelativeValidatorPerformanceData
    {
        /// <summary>
        /// Validator public key represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        /// <summary>
        /// Era identifier.
        /// </summary>
        [JsonProperty("era_id")]
        public uint? EraId { get; set; }

        /// <summary>
        /// Validator performance score for the era.
        /// </summary>
        [JsonProperty("score")]
        public double? Score { get; set; }

    }
}
