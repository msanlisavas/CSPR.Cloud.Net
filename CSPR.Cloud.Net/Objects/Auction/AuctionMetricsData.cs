using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Auction
{
    public class AuctionMetricsData
    {
        [JsonProperty("current_era_id")]
        public uint? CurrentEraId { get; set; }

        [JsonProperty("active_validator_number")]
        public uint? ActiveValidatorNumber { get; set; }

        [JsonProperty("total_bids_number")]
        public uint? TotalBidsNumber { get; set; }

        [JsonProperty("active_bids_number")]
        public uint? ActiveBidsNumber { get; set; }

        [JsonProperty("total_active_era_stake")]
        public ulong? TotalActiveEraStake { get; set; }
    }
}
