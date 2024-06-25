using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Validator
{
    public class ValidatorRewardData
    {
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
        [JsonProperty("era_id")]
        public uint? EraId { get; set; } // Era identifier

        [JsonProperty("amount")]
        public ulong? Amount { get; set; } // Amount of the reward received by the validator

        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; } // Timestamp indicating when the last block in the era was proposed
        [JsonProperty("rate")]
        public float? Rate { get; set; } // Reward rate

    }
}
