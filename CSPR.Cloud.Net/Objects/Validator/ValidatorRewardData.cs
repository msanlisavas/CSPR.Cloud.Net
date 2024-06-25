using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Validator
{
    /// <summary>
    /// The ValidatorReward entity offers a normalized representation of the Casper Network Reward received by a Validator for a specific era.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator-reward">CSPR Cloud API documentation</see>.
    /// </summary>
    public class ValidatorRewardData
    {
        /// <summary>
        /// Public key of the validator represented as a hexadecimal string. Unique validator identifier.
        /// </summary>
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        /// <summary>
        /// Era identifier.
        /// </summary>
        [JsonProperty("era_id")]
        public uint? EraId { get; set; }

        /// <summary>
        /// Amount of the reward received by the validator.
        /// </summary>
        [JsonProperty("amount")]
        public ulong? Amount { get; set; }

        /// <summary>
        /// Timestamp indicating when the last block in the era was proposed.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// Reward rate.
        /// </summary>
        [JsonProperty("rate")]
        public float? Rate { get; set; }
    }

}
