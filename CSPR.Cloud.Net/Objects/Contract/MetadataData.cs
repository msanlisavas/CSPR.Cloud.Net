using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Contract
{
    public class MetadataData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("decimals")]
        public int Decimals { get; set; }

        [JsonProperty("balances_uref")]
        public string BalancesUref { get; set; }

        [JsonProperty("total_supply_uref")]
        public string TotalSupplyUref { get; set; }

        [JsonProperty("ownership_mode")]
        public int OwnershipMode { get; set; }

        [JsonProperty("nft_kind")]
        public int NftKind { get; set; }

        [JsonProperty("nft_metadata_kind")]
        public int NftMetadataKind { get; set; }

        [JsonProperty("whitelist_mode")]
        public int WhitelistMode { get; set; }

        [JsonProperty("holder_mode")]
        public int HolderMode { get; set; }

        [JsonProperty("minting_mode")]
        public int MintingMode { get; set; }

        [JsonProperty("burn_mode")]
        public int BurnMode { get; set; }

        [JsonProperty("identifier_mode")]
        public int IdentifierMode { get; set; }

        [JsonProperty("metadata_mutability")]
        public int MetadataMutability { get; set; }

        [JsonProperty("owner_reverse_lookup_mode")]
        public int OwnerReverseLookupMode { get; set; }

        [JsonProperty("events_mode")]
        public int EventsMode { get; set; }
    }
}
