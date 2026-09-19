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
        /// <remarks>The wire name is <c>staker_identifier</c>; the endpoint has never emitted
        /// <c>delegator_identifier</c>, which this bound to before v4.3.0 (always null).</remarks>
        [JsonProperty("staker_identifier")]
        public string DelegatorIdentifier { get; set; }

        /// <summary>
        /// Identifier type: 0 for public-key delegators, 1 for purse delegators (v2.1.0+).
        /// </summary>
        /// <remarks>The wire name is <c>withdrawal_type_id</c>. Before v4.3.0 this bound to
        /// <c>delegator_identifier_type_id</c>, which the endpoint does not emit — and because the
        /// property is non-nullable it read 0 on every row, silently reporting every purse
        /// withdrawal as a public-key one. (Making it <c>int?</c> so "absent" stays distinguishable
        /// from a real 0 is deferred: it would be a source-breaking change.)</remarks>
        [JsonProperty("withdrawal_type_id")]
        public int DelegatorIdentifierTypeId { get; set; }

        /// <summary>
        /// Era in which the undelegation was created. Funds are released 7 eras later.
        /// </summary>
        /// <remarks>The wire name is <c>era_id</c>; <c>era_of_creation</c> is never emitted, so the
        /// 7-era release calculation this doc promises was uncomputable before v4.3.0.</remarks>
        [JsonProperty("era_id")]
        public ulong? EraOfCreation { get; set; }

        /// <summary>
        /// Timestamp when the undelegation was initiated.
        /// </summary>
        [JsonProperty("timestamp")]
        public System.DateTime? Timestamp { get; set; }

        /// <summary>
        /// Never populated by this endpoint. Verified live: requesting <c>includes=account_info</c>
        /// leaves the response unchanged, while <c>includes=validator_account_info</c> does add its
        /// object — so the includer is unsupported here rather than merely unset.
        /// Use <see cref="ValidatorAccountInfo"/>, or look the delegator up via the accounts endpoint.
        /// </summary>
        [System.Obsolete("Never populated: /accounts/{publicKey}/undelegations does not support the account_info includer. Use ValidatorAccountInfo, or fetch the account separately.")]
        [JsonProperty("account_info")]
        public AccountInfoData AccountInfo { get; set; }

        /// <summary>
        /// Additional account information of the validator (included when the <c>validator_account_info</c> flag is set).
        /// </summary>
        [JsonProperty("validator_account_info")]
        public AccountInfoData ValidatorAccountInfo { get; set; }

        /// <summary>
        /// Never populated by this endpoint — same verification as <see cref="AccountInfo"/>.
        /// </summary>
        [System.Obsolete("Never populated: /accounts/{publicKey}/undelegations does not support the centralized_account_info includer.")]
        [JsonProperty("centralized_account_info")]
        public CentralizedAccountInfoData CentralizedAccountInfo { get; set; }
    }
}
