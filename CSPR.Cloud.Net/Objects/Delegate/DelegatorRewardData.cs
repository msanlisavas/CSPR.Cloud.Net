using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Delegate
{
    /// <summary>
    /// The DelegatorReward entity offers a normalized representation of the Casper Network Reward related to the delegator account.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/delegator-reward">CSPR Cloud API documentation</see>.
    /// </summary>
    public class DelegatorRewardData
    {
        /// <summary>
        /// Amount of the reward received by the delegator.
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Era identifier.
        /// </summary>
        [JsonProperty("era_id")]
        public int? EraId { get; set; }

        /// <summary>
        /// The public key of the delegator account represented as a hexadecimal string. Primary account identifier.
        /// </summary>
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        /// <summary>
        /// Timestamp indicating when the last block was proposed.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// Public key of the validator represented as a hexadecimal string. Unique validator identifier.
        /// </summary>
        [JsonProperty("validator_public_key")]
        public string ValidatorPublicKey { get; set; }

        /// <summary>
        /// Rate of the reward.
        /// </summary>
        [JsonProperty("rate")]
        public float? Rate { get; set; }

        /// <summary>
        /// Additional account information of the delegator.
        /// </summary>
        [JsonProperty("account_info")]
        public AccountInfoData AccountInfo { get; set; }

        /// <summary>
        /// Centralized account information of the delegator.
        /// </summary>
        [JsonProperty("centralized_account_info")]
        public CentralizedAccountInfoData CentralizedAccountInfo { get; set; }
    }

}
