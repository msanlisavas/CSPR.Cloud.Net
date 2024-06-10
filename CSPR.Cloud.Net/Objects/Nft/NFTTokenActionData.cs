﻿using CSPR.Cloud.Net.Enums;
using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Objects.Nft
{
    public class NFTTokenActionData
    {
        [JsonProperty("deploy_hash")]
        public string DeployHash { get; set; }

        [JsonProperty("block_height")]
        public ulong BlockHeight { get; set; }

        [JsonProperty("token_tracking_id")]
        public string TokenTrackingId { get; set; }

        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        [JsonProperty("from_hash")]
        public string FromHash { get; set; }

        [JsonProperty("from_type")]
        public HashType? FromType { get; set; }

        [JsonProperty("to_hash")]
        public string ToHash { get; set; }

        [JsonProperty("to_type")]
        public HashType? ToType { get; set; }

        [JsonProperty("nft_action_id")]
        public uint NftActionId { get; set; }

        [JsonProperty("token_id")]
        public string TokenId { get; set; }

        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }
    }
}