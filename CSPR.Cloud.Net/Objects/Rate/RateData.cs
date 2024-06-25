using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Rate
{
    /// <summary>
    /// The Rate entity represents historical CSPR rates provided by CoinGecko for more than 40 different fiat and crypto currencies.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/cspr-rate">CSPR Cloud API documentation</see>.
    /// </summary>
    public class RateData
    {
        /// <summary>
        /// Currency identifier.
        /// </summary>
        [JsonProperty("currency_id")]
        public byte CurrencyId { get; set; }

        /// <summary>
        /// Rate amount.
        /// </summary>
        [JsonProperty("amount")]
        public float? Amount { get; set; }

        /// <summary>
        /// Rate timestamp in the ISO 8601 format.
        /// </summary>
        [JsonProperty("created")]
        public DateTime? Created { get; set; }
    }

}
