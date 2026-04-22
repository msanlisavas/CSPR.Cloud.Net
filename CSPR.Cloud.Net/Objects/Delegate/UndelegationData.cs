using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Delegate
{
    /// <summary>
    /// The Undelegation entity represents a pending undelegation record on Casper Network — funds that
    /// have been requested to be undelegated but are still within the 7-era lockup period before being
    /// returned to the delegator's main purse.
    /// <para>
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/delegation">CSPR Cloud API documentation</see>.
    /// </para>
    /// </summary>
    public class UndelegationData
    {
        /// <summary>
        /// Public key of the delegator represented as a hexadecimal string.
        /// Null when the undelegation was initiated from a purse (see <see cref="DelegatorIdentifierTypeId"/>).
        /// </summary>
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        /// <summary>
        /// Public key of the validator the funds were delegated to, represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("validator_public_key")]
        public string ValidatorPublicKey { get; set; }

        /// <summary>
        /// Amount of motes being undelegated. Typed as string to avoid uint64 overflow.
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// URef of the purse that originally bonded the delegation, in the uref-dead...beef-007 format.
        /// </summary>
        [JsonProperty("bonding_purse")]
        public string BondingPurse { get; set; }

        /// <summary>
        /// Identifier of the delegator — either a public key (when <see cref="DelegatorIdentifierTypeId"/> is 0)
        /// or a purse URef (when <see cref="DelegatorIdentifierTypeId"/> is 1).
        /// </summary>
        [JsonProperty("delegator_identifier")]
        public string DelegatorIdentifier { get; set; }

        /// <summary>
        /// Identifier type: 0 for public-key delegators, 1 for purse delegators (v2.1.0+).
        /// </summary>
        [JsonProperty("delegator_identifier_type_id")]
        public int DelegatorIdentifierTypeId { get; set; }

        /// <summary>
        /// Era in which the undelegation was created. Funds are released 7 eras later.
        /// </summary>
        [JsonProperty("era_of_creation")]
        public ulong? EraOfCreation { get; set; }

        /// <summary>
        /// Timestamp when the undelegation was initiated.
        /// </summary>
        [JsonProperty("timestamp")]
        public System.DateTime? Timestamp { get; set; }

        /// <summary>
        /// Additional account information of the delegator (included when the <c>account_info</c> flag is set).
        /// </summary>
        [JsonProperty("account_info")]
        public AccountInfoData AccountInfo { get; set; }

        /// <summary>
        /// Additional account information of the validator (included when the <c>validator_account_info</c> flag is set).
        /// </summary>
        [JsonProperty("validator_account_info")]
        public AccountInfoData ValidatorAccountInfo { get; set; }

        /// <summary>
        /// Centralized account information (included when the <c>centralized_account_info</c> flag is set).
        /// </summary>
        [JsonProperty("centralized_account_info")]
        public CentralizedAccountInfoData CentralizedAccountInfo { get; set; }
    }
}
