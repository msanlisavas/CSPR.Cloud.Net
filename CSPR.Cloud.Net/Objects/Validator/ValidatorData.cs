using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Validator
{
    /// <summary>
    /// The Validator entity provides a normalized representation of the Casper Network Validator.
    /// It includes the extended information about its stakes, fee, shares, etc.
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
        /// </summary>
        [JsonProperty("self_stake")]
        public ulong? SelfStake { get; set; }

        /// <summary>
        /// Cumulative stake of all delegators.
        /// </summary>
        [JsonProperty("delegators_stake")]
        public ulong? DelegatorsStake { get; set; }

        /// <summary>
        /// Total validator stake. The sum of the self-stake and the delegator stakes.
        /// </summary>
        [JsonProperty("total_stake")]
        public ulong? TotalStake { get; set; }

        /// <summary>
        /// Percentage of the validator's self-stake to its total stake.
        /// </summary>
        [JsonProperty("self_share")]
        public float? SelfShare { get; set; }

        /// <summary>
        /// Percentage of the validator's total stake to the total amount staked on the network.
        /// </summary>
        [JsonProperty("network_share")]
        public float? NetworkShare { get; set; }

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
    }

}
