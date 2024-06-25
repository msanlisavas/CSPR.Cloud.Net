using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Contract
{
    /// <summary>
    /// Represents metadata associated with the contract package.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract-package">CSPR Cloud API documentation</see>.
    /// </summary>
    public class MetadataData
    {
        /// <summary>
        /// Name associated with the contract package.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Contract package name pulled from the last version named keys. Contract package symbol pulled from the last version named keys. Applicable only for token contracts.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Contract package token decimals pulled from the last version named keys. Applicable only for token contracts.
        /// </summary>
        [JsonProperty("decimals")]
        public int? Decimals { get; set; }

        /// <summary>
        /// Contract package balances dictionary URef pulled from the last version named keys. Applicable only for token contracts.
        /// </summary>
        [JsonProperty("balances_uref")]
        public string BalancesUref { get; set; }

        /// <summary>
        /// Contract package total supply URef pulled from the last version named keys. Applicable only for token contracts.
        /// </summary>
        [JsonProperty("total_supply_uref")]
        public string TotalSupplyUref { get; set; }

        /// <summary>
        /// CEP-78 contract ownership mode. Applicable only for CEP-78 contracts.
        /// </summary>
        [JsonProperty("ownership_mode")]
        public int? OwnershipMode { get; set; }

        /// <summary>
        /// CEP-78 contract NFT kind. Applicable only for CEP-78 contracts.
        /// </summary>
        [JsonProperty("nft_kind")]
        public int? NftKind { get; set; }

        /// <summary>
        /// CEP-78 contract NFT metadata kind. Applicable only for CEP-78 contracts.
        /// </summary>
        [JsonProperty("nft_metadata_kind")]
        public int? NftMetadataKind { get; set; }

        /// <summary>
        /// CEP-78 contract whitelist mode. Applicable only for CEP-78 contracts.
        /// </summary>
        [JsonProperty("whitelist_mode")]
        public int? WhitelistMode { get; set; }

        /// <summary>
        /// CEP-78 contract holder mode. Applicable only for CEP-78 contracts.
        /// </summary>
        [JsonProperty("holder_mode")]
        public int? HolderMode { get; set; }

        /// <summary>
        /// CEP-78 contract minting mode. Applicable only for CEP-78 contracts.
        /// </summary>
        [JsonProperty("minting_mode")]
        public int? MintingMode { get; set; }

        /// <summary>
        /// CEP-78 contract burn mode. Applicable only for CEP-78 contracts.
        /// </summary>
        [JsonProperty("burn_mode")]
        public int? BurnMode { get; set; }

        /// <summary>
        /// CEP-78 contract identifier mode. Applicable only for CEP-78 contracts.
        /// </summary>
        [JsonProperty("identifier_mode")]
        public int? IdentifierMode { get; set; }

        /// <summary>
        /// CEP-78 contract metadata mutability mode. Applicable only for CEP-78 contracts.
        /// </summary>
        [JsonProperty("metadata_mutability")]
        public int? MetadataMutability { get; set; }

        /// <summary>
        /// CEP-78 contract owner reverse lookup mode. Applicable only for CEP-78 contracts.
        /// </summary>
        [JsonProperty("owner_reverse_lookup_mode")]
        public int? OwnerReverseLookupMode { get; set; }

        /// <summary>
        /// CEP-78 contract events mode. Applicable only for CEP-78 contracts.
        /// </summary>
        [JsonProperty("events_mode")]
        public int? EventsMode { get; set; }
    }

}
