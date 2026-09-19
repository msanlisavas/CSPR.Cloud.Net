using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Validator
{
    /// <summary>
    /// The ValidatorPerformance entity provides the validator performance score as the percentage of rewards received per one token relative to the maximum ratio received by a validator in the given era.
    /// A validator that received the highest ratio will have a 100 score.
    /// The average performance endpoints return moving average of the validator performance for the past 360 eras (approximately one month).
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator-performance">CSPR Cloud API documentation</see>.
    /// </summary>
    public class ValidatorPerformanceData
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
        /// Validator performance score for the era — the moving average over the past 360 eras.
        /// </summary>
        /// <remarks>
        /// The wire name is <c>score</c>, not <c>average_score</c>: <c>/validators/{publicKey}/relative-average-performances</c>
        /// returns <c>{"public_key":…,"era_id":…,"score":99.96}</c>. The property keeps the name
        /// <c>AverageScore</c> because that is what the value means on this endpoint — it is the
        /// average — and renaming it would break callers.
        /// </remarks>
        [JsonProperty("score")]
        public double? AverageScore { get; set; }
    }

}
