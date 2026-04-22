using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using CSPR.Cloud.Net.Objects.Validator;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Bidder
{
    /// <summary>
    /// The Bidder entity represents an account participating in the auction to become a Validator for the era after the next one.
    /// It has the same properties as the Validator entity, with the network share value being projected instead of actual.
    /// <para>
    /// Stake and delegation-amount fields are typed as <see cref="string"/> to avoid uint64 overflow (v2.4.3+).
    /// </para>
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/bidder">CSPR Cloud API documentation</see>.
    /// </summary>
    public class BidderData
    {
        /// <summary>
        /// Percentage of the rewards the bidder will retain for its services once becomes a validator.
        /// </summary>
        [JsonProperty("fee")]
        public int? Fee { get; set; }

        /// <summary>
        /// Describes whether the bidder is active or not.
        /// </summary>
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Current era identifier in which the bidder is being observed.
        /// </summary>
        [JsonProperty("era_id")]
        public uint? EraId { get; set; }

        /// <summary>
        /// Projected network stake share if the bidder becomes a validator.
        /// </summary>
        [JsonProperty("network_share")]
        public string NetworkShare { get; set; }

        /// <summary>
        /// Bidder account public key represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        /// <summary>
        /// Projected validator rank based on total bid amount.
        /// </summary>
        [JsonProperty("rank")]
        public int? Rank { get; set; }

        /// <summary>
        /// Percentage of the bidder's self-stake to its total stake.
        /// </summary>
        [JsonProperty("self_share")]
        public string SelfShare { get; set; }

        /// <summary>
        /// Bidder self-stake — sum of the bidder's own bid plus stakes of affiliated accounts from the Casper Account Info Standard.
        /// Typed as string (v2.4.3+).
        /// </summary>
        [JsonProperty("self_stake")]
        public string SelfStake { get; set; }

        /// <summary>
        /// Total bidder stake — the sum of the self-stake and the delegator stakes. Typed as string (v2.4.3+).
        /// </summary>
        [JsonProperty("total_stake")]
        public string TotalStake { get; set; }

        /// <summary>
        /// Raw bid amount in motes (without affiliated-account bonus).
        /// </summary>
        [JsonProperty("bid_amount")]
        public string BidAmount { get; set; }

        /// <summary>
        /// Number of delegators staked to this bidder.
        /// </summary>
        [JsonProperty("delegators_number")]
        public ulong? DelegatorsNumber { get; set; }

        /// <summary>
        /// Cumulative stake of all delegators.
        /// </summary>
        [JsonProperty("delegators_stake")]
        public string DelegatorsStake { get; set; }

        /// <summary>
        /// Number of reserved delegation slots held by the bidder (Casper 2.0).
        /// </summary>
        [JsonProperty("reserved_slots")]
        public uint? ReservedSlots { get; set; }

        /// <summary>
        /// Minimum delegation amount accepted by the bidder, in motes.
        /// </summary>
        [JsonProperty("minimum_delegation_amount")]
        public string MinimumDelegationAmount { get; set; }

        /// <summary>
        /// Maximum delegation amount accepted by the bidder, in motes.
        /// </summary>
        [JsonProperty("maximum_delegation_amount")]
        public string MaximumDelegationAmount { get; set; }

        /// <summary>
        /// Additional account information.
        /// </summary>
        [JsonProperty("account_info")]
        public AccountInfoData AccountInfo { get; set; }

        /// <summary>
        /// Average performance data of the bidder.
        /// </summary>
        [JsonProperty("average_performance")]
        public ValidatorPerformanceData AveragePerformance { get; set; }

        /// <summary>
        /// Centralized account information.
        /// </summary>
        [JsonProperty("centralized_account_info")]
        public CentralizedAccountInfoData CentralizedAccountInfo { get; set; }

        /// <summary>
        /// The bidder's registered CSPR.name when the <c>cspr_name</c> includer is requested (v2.1.0+).
        /// </summary>
        [JsonProperty("cspr_name")]
        public string CsprName { get; set; }
    }

}
