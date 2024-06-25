using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using CSPR.Cloud.Net.Objects.Validator;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Bidder
{
    /// <summary>
    /// The Bidder entity represents an account participating in the auction to become a Validator for the era after the next one.
    /// It has the same properties as the Validator entity, with the network share value being projected instead of actual.
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
        /// Bidder self-stake calculated as a sum of the bidder stake and the stakes of the affiliated accounts provided via the Casper Account Info Standard.
        /// </summary>
        [JsonProperty("self_stake")]
        public ulong? SelfStake { get; set; }

        /// <summary>
        /// Total bidder stake. The sum of the self-stake and the delegator stakes.
        /// </summary>
        [JsonProperty("total_stake")]
        public ulong? TotalStake { get; set; }

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
    }

}
