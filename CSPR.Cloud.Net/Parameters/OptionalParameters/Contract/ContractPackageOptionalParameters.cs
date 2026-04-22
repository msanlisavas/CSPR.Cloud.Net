using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Contract
{
    /// <summary>
    /// Represents optional parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/optional-properties">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class ContractPackageOptionalParameters
    {
        /// <summary>
        /// Gets or sets the number of deploys in the specified number of the past days.
        /// This property accepts the number of days as an argument.
        /// </summary>
        [JsonProperty("deploys_number")]
        public int? DeploysNumber { get; set; } = 0;

        /// <summary>
        /// Includes token market data for FT contract packages (v2.9.0+).
        /// Function includer — pass the currency id (e.g. 1 for USD) to enable it.
        /// </summary>
        [JsonProperty("token_market_data")]
        public int? TokenMarketData { get; set; } = 0;

        /// <summary>
        /// Includes CoinGecko market metadata when available (v2.1.0+).
        /// </summary>
        [JsonProperty("coingecko_data")]
        public bool CoingeckoData { get; set; } = false;

        /// <summary>
        /// Includes FriendlyMarket metadata when available (v2.1.1+).
        /// </summary>
        [JsonProperty("friendlymarket_data")]
        public bool FriendlymarketData { get; set; } = false;

        /// <summary>
        /// Includes CSPR.trade data when available (v2.7.0+).
        /// </summary>
        [JsonProperty("csprtrade_data")]
        public bool CsprtradeData { get; set; } = false;

        /// <summary>
        /// Includes the owner's CSPR.name when one is registered (v2.1.0+).
        /// </summary>
        [JsonProperty("owner_cspr_name")]
        public bool OwnerCsprName { get; set; } = false;
    }
}
