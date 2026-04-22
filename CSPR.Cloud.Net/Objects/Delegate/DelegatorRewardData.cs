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

        /// <summary>
        /// Delegator identifier — either a public key (type 0) or a purse URef (type 1). Added with v2.1.0 purse-delegation support.
        /// </summary>
        [JsonProperty("delegator_identifier")]
        public string DelegatorIdentifier { get; set; }

        /// <summary>
        /// Delegator identifier type: 0 for public-key delegators, 1 for purse delegators (v2.1.0+).
        /// </summary>
        [JsonProperty("delegator_identifier_type_id")]
        public int? DelegatorIdentifierTypeId { get; set; }

        /// <summary>
        /// Validator account information when the <c>validator_account_info</c> includer is requested.
        /// </summary>
        [JsonProperty("validator_account_info")]
        public AccountInfoData ValidatorAccountInfo { get; set; }

        /// <summary>
        /// The delegator's registered CSPR.name when the <c>cspr_name</c> includer is requested (v2.1.0+).
        /// </summary>
        [JsonProperty("cspr_name")]
        public string CsprName { get; set; }

        /// <summary>
        /// The validator's registered CSPR.name when the <c>validator_cspr_name</c> includer is requested (v2.1.0+).
        /// </summary>
        [JsonProperty("validator_cspr_name")]
        public string ValidatorCsprName { get; set; }
    }

}
