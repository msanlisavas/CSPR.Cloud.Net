using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Auction
{
    /// <summary>
    /// The AuctionMetrics entity represents calculated metrics for the era retrieved from auction info.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/auction-metrics">CSPR Cloud API documentation</see>.
    /// </summary>
    public class AuctionMetricsData
    {
        /// <summary>
        /// Current era identifier.
        /// </summary>
        [JsonProperty("current_era_id")]
        public uint? CurrentEraId { get; set; }

        /// <summary>
        /// Current number of active validators.
        /// </summary>
        [JsonProperty("active_validator_number")]
        public uint? ActiveValidatorNumber { get; set; }

        /// <summary>
        /// Total number of bidders.
        /// </summary>
        [JsonProperty("total_bids_number")]
        public uint? TotalBidsNumber { get; set; }

        /// <summary>
        /// Number of active bidders.
        /// </summary>
        [JsonProperty("active_bids_number")]
        public uint? ActiveBidsNumber { get; set; }

        /// <summary>
        /// Total sum of all validator stakes from current and next era.
        /// </summary>
        [JsonProperty("total_active_era_stake")]
        public ulong? TotalActiveEraStake { get; set; }
    }

}
