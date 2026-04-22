using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Validator
{
    /// <summary>
    /// The Validator entity provides a normalized representation of the Casper Network Validator.
    /// It includes the extended information about its stakes, fee, shares, etc.
    /// <para>
    /// Stake and delegation-amount fields are typed as <see cref="string"/> because the API emits
    /// them as JSON strings to avoid uint64 overflow in clients that don't support 64-bit unsigned
    /// integers (v2.4.3+).
    /// </para>
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator">CSPR Cloud API documentation</see>.
    /// </summary>
    public class ValidatorData
    {
        /// <summary>
        /// Projected validator rank based on total stake amount.
        /// </summary>
        [JsonProperty("rank")]
        public uint? Rank { get; set; }

        /// <summary>
        /// Current era validator identifier.
        /// </summary>
        [JsonProperty("era_id")]
        public uint? EraId { get; set; }

        /// <summary>
        /// Validator public key represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        /// <summary>
        /// Describes whether the validator is active or not.
        /// </summary>
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Percentage of the rewards the validator retains for its services.
        /// </summary>
        [JsonProperty("fee")]
        public float? Fee { get; set; }

        /// <summary>
        /// Number of delegators staked to validator.
        /// </summary>
        [JsonProperty("delegators_number")]
        public ulong? DelegatorsNumber { get; set; }

        /// <summary>
        /// Validator self-stake calculated as a sum of the bidder stake and the stakes of the affiliated accounts provided via the Casper Account Info Standard.
        /// Typed as string (v2.4.3+).
        /// </summary>
        [JsonProperty("self_stake")]
        public string SelfStake { get; set; }

        /// <summary>
        /// Cumulative stake of all delegators. Typed as string (v2.4.3+).
        /// </summary>
        [JsonProperty("delegators_stake")]
        public string DelegatorsStake { get; set; }

        /// <summary>
        /// Total validator stake — the sum of the self-stake and the delegator stakes. Typed as string (v2.4.3+).
        /// </summary>
        [JsonProperty("total_stake")]
        public string TotalStake { get; set; }

        /// <summary>
        /// Validator bid amount in motes — the self-stake without the affiliated-account bonus (v2.0.5+).
        /// </summary>
        [JsonProperty("bid_amount")]
        public string BidAmount { get; set; }

        /// <summary>
        /// Number of reserved delegation slots held by the validator (Casper 2.0).
        /// </summary>
        [JsonProperty("reserved_slots")]
        public uint? ReservedSlots { get; set; }

        /// <summary>
        /// Minimum delegation amount accepted by the validator, in motes. Typed as string to avoid uint64 overflow.
        /// </summary>
        [JsonProperty("minimum_delegation_amount")]
        public string MinimumDelegationAmount { get; set; }

        /// <summary>
        /// Maximum delegation amount accepted by the validator, in motes. Typed as string to avoid uint64 overflow.
        /// </summary>
        [JsonProperty("maximum_delegation_amount")]
        public string MaximumDelegationAmount { get; set; }

        /// <summary>
        /// Pending amount the validator is unstaking in the current unbonding window (v2.0.5+).
        /// </summary>
        [JsonProperty("pending_unstaking_amount")]
        public string PendingUnstakingAmount { get; set; }

        /// <summary>
        /// Percentage of the validator's self-stake to its total stake. Typed as string to preserve server-side precision.
        /// </summary>
        [JsonProperty("self_share")]
        public string SelfShare { get; set; }

        /// <summary>
        /// Percentage of the validator's total stake to the total amount staked on the network.
        /// Typed as string to preserve server-side precision.
        /// </summary>
        [JsonProperty("network_share")]
        public string NetworkShare { get; set; }

        /// <summary>
        /// Validator account information.
        /// </summary>
        [JsonProperty("account_info")]
        public AccountInfoData AccountInfo { get; set; }

        /// <summary>
        /// Validator centralized account information.
        /// </summary>
        [JsonProperty("centralized_account_info")]
        public CentralizedAccountInfoData CentralizedAccountInfo { get; set; }

        /// <summary>
        /// Average performance of the validator in the last 30 days.
        /// </summary>
        [JsonProperty("average_performance")]
        public ValidatorPerformanceData AveragePerformance { get; set; }

        /// <summary>
        /// The validator's registered CSPR.name when the <c>cspr_name</c> includer is requested (v2.1.0+).
        /// </summary>
        [JsonProperty("cspr_name")]
        public string CsprName { get; set; }
    }

}
