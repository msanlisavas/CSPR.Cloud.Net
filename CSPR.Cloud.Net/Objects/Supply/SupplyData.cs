using Newtonsoft.Json;
using System;

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
        /// The latest timestamp when the supply value was updated.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }
    }

}
