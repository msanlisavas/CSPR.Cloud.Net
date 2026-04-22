using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Supply
{
    /// <summary>
    /// The Supply entity holds information about total and circulating supply of the Mainnet CSPR token.
    /// The circulating supply is calculated according to the approach described on the Casper website.
    /// At this moment, the API returns correct circulating supply only on the Mainnet.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/cspr-supply">CSPR Cloud API documentation</see>.
    /// </summary>
    public class SupplyData
    {
        /// <summary>
        /// Token represented by the supply data. At this point, the value is always CSPR.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Total available supply of the token.
        /// </summary>
        [JsonProperty("total")]
        public ulong? Total { get; set; }

        /// <summary>
        /// Circulating supply of the token calculated according to the approach described on the Casper website.
        /// </summary>
        [JsonProperty("circulating")]
        public ulong? Circulating { get; set; }

        /// <summary>
        /// Total annual issuance rate of the native token as a fraction of the total supply (v2.4.0+).
        /// </summary>
        [JsonProperty("total_annual_issuance")]
        public double? TotalAnnualIssuance { get; set; }

        /// <summary>
        /// Portion of the annual issuance allocated to ecosystem sustainability (v2.8.0+).
        /// </summary>
        [JsonProperty("annual_ecosystem_sustain_issuance")]
        public double? AnnualEcosystemSustainIssuance { get; set; }

        /// <summary>
        /// Portion of the annual issuance allocated to staking rewards (v2.8.0+).
        /// </summary>
        [JsonProperty("annual_staking_rewards_issuance")]
        public double? AnnualStakingRewardsIssuance { get; set; }

        /// <summary>
        /// Deprecated alias retained for backward compatibility — equivalent to <see cref="TotalAnnualIssuance"/>.
        /// </summary>
        [JsonProperty("annual_issuance")]
        public double? AnnualIssuance { get; set; }

        /// <summary>
        /// Unix epoch (seconds) of the latest supply update. The Supply endpoint emits this as a
        /// numeric timestamp rather than the ISO-8601 string other endpoints use.
        /// </summary>
        [JsonProperty("timestamp")]
        public long? Timestamp { get; set; }
    }

}
