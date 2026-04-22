using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.Bidder;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Delegate
{
    /// <summary>
    /// The Delegation entity represents a record of a delegation transaction in the context of Casper Network Staking vs. Delegating process.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/delegation">CSPR Cloud API documentation</see>.
    /// </summary>
    public class DelegationData
    {
        /// <summary>
        /// Public key of the delegator represented as a hexadecimal string. Null when the delegator is a purse (see <see cref="DelegatorIdentifierTypeId"/>).
        /// </summary>
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        /// <summary>
        /// Public key of the validator represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("validator_public_key")]
        public string ValidatorPublicKey { get; set; }

        /// <summary>
        /// Delegation amount in motes. Typed as string to avoid uint64 overflow.
        /// </summary>
        [JsonProperty("stake")]
        public string Stake { get; set; }

        /// <summary>
        /// URef of the purse from which the delegation was made in the uref-dead...beef-007 format.
        /// </summary>
        [JsonProperty("bonding_purse")]
        public string BondingPurse { get; set; }

        /// <summary>
        /// Delegator identifier — either a public key (when <see cref="DelegatorIdentifierTypeId"/> is 0)
        /// or a purse URef (when <see cref="DelegatorIdentifierTypeId"/> is 1). Added with v2.1.0 purse-delegation support.
        /// </summary>
        [JsonProperty("delegator_identifier")]
        public string DelegatorIdentifier { get; set; }

        /// <summary>
        /// Delegator identifier type: 0 for public-key delegators, 1 for purse delegators (v2.1.0+).
        /// </summary>
        [JsonProperty("delegator_identifier_type_id")]
        public int? DelegatorIdentifierTypeId { get; set; }

        /// <summary>
        /// Additional account information of the delegator.
        /// </summary>
        [JsonProperty("account_info")]
        public AccountInfoData AccountInfo { get; set; }

        /// <summary>
        /// Additional account information of the validator.
        /// </summary>
        [JsonProperty("validator_account_info")]
        public AccountInfoData ValidatorAccountInfo { get; set; }

        /// <summary>
        /// Centralized account information.
        /// </summary>
        [JsonProperty("centralized_account_info")]
        public CentralizedAccountInfoData CentralizedAccountInfo { get; set; }

        /// <summary>
        /// Bidder record for the delegator when the <c>bidder</c> includer is requested (v2.1.0+).
        /// </summary>
        [JsonProperty("bidder")]
        public BidderData Bidder { get; set; }

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
