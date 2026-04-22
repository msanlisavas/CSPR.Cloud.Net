using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Objects.Contract
{
    /// <summary>
    /// The ContractPackage entity provides a normalized representation of the Casper Network Contract Package.
    /// It includes the contract package's metadata based on Contract's type and named keys values and the latest version of the contract type within the package.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract-package">CSPR Cloud API documentation</see>.
    /// </summary>
    public class ContractPackageData
    {
        /// <summary>
        /// Contract package hash represented as a hexadecimal string. Unique contract package identifier.
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        /// <summary>
        /// Public key of the owner of the contract package, represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("owner_public_key")]
        public string OwnerPublicKey { get; set; }

        /// <summary>
        /// Account hash of the owner when the owner is identified by hash (not always present alongside <see cref="OwnerPublicKey"/>).
        /// </summary>
        [JsonProperty("owner_hash")]
        public string OwnerHash { get; set; }

        /// <summary>
        /// Name of the contract package.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the contract package.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Metadata associated with the contract package.
        /// </summary>
        [JsonProperty("metadata")]
        public MetadataData Metadata { get; set; }

        /// <summary>
        /// Contract type identifier of the latest contract version.
        /// </summary>
        [JsonProperty("latest_version_contract_type_id")]
        public int? LatestVersionContractTypeId { get; set; }

        /// <summary>
        /// Timestamp indicating when the contract package was created.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// URL pointing to an icon representing the contract package.
        /// </summary>
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        /// <summary>
        /// Public website URL for the contract package (v2.0.12+).
        /// </summary>
        [JsonProperty("website_url")]
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// Number of deploys associated with the contract package.
        /// </summary>
        [JsonProperty("deploys_number")]
        public int? DeploysNumber { get; set; }

        /// <summary>
        /// True when the contract package has been reviewed and its info is approved.
        /// </summary>
        [JsonProperty("is_contract_info_approved")]
        public bool? IsContractInfoApproved { get; set; }

        /// <summary>
        /// True when the contract package is featured in CSPR.cloud listings.
        /// </summary>
        [JsonProperty("is_featured")]
        public bool? IsFeatured { get; set; }

        /// <summary>
        /// CoinGecko coin identifier — populated when the FT contract package is tracked by CoinGecko.
        /// </summary>
        [JsonProperty("coingecko_id")]
        public string CoingeckoId { get; set; }

        /// <summary>
        /// Owner's registered CSPR.name when the <c>owner_cspr_name</c> includer is requested (v2.1.0+).
        /// </summary>
        [JsonProperty("owner_cspr_name")]
        public string OwnerCsprName { get; set; }

        /// <summary>
        /// Token market data array when the <c>token_market_data</c> function includer is requested (v2.9.0+).
        /// Shape depends on the currency and dex parameters passed to the includer — exposed as raw JSON for flexibility.
        /// </summary>
        [JsonProperty("token_market_data")]
        public List<JObject> TokenMarketData { get; set; }

        /// <summary>
        /// CoinGecko market data object when the <c>coingecko_data</c> includer is requested (v2.1.0+).
        /// Shape mirrors CoinGecko's public API — exposed as raw JSON.
        /// </summary>
        [JsonProperty("coingecko_data")]
        public JObject CoingeckoData { get; set; }

        /// <summary>
        /// FriendlyMarket metadata when the <c>friendlymarket_data</c> includer is requested (v2.1.1+).
        /// </summary>
        [JsonProperty("friendlymarket_data")]
        public JObject FriendlymarketData { get; set; }

        /// <summary>
        /// CSPR.trade data when the <c>csprtrade_data</c> includer is requested (v2.7.0+).
        /// </summary>
        [JsonProperty("csprtrade_data")]
        public JObject CsprtradeData { get; set; }
    }
}
