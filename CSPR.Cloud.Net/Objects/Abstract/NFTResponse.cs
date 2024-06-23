using CSPR.Cloud.Net.Enums;
using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Abstract
{
    public class NFTResponse<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("owner_hash")]
        public string OwnerHash { get; set; } // NFT token owner account or contract hash, represented as a hexadecimal string

        [JsonProperty("owner_type")]
        public OwnerType OwnerType { get; set; } // NFT owner type. 0 for account, 1 for contract
        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; } // The timestamp of token creation
        [JsonProperty("token_id")]
        public string TokenId { get; set; } // Token ID under the contract. Second part of token identifier

        [JsonProperty("token_standard_id")]
        public byte TokenStandardId { get; set; } // NFT standard identifier
        [JsonProperty("tracking_id")]
        public string TrackingId { get; set; } // NFT token tracking identifier


    }
}
