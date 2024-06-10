﻿using CSPR.Cloud.Net.Enums;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Bidder
{
    public class BiddersSortingParameters
    {
        [JsonProperty("rank")]
        public bool OrderByRank { get; set; } = false;

        [JsonProperty("fee")]
        public bool OrderByFee { get; set; } = false;
        [JsonProperty("delegators_number")]
        public bool OrderByDelegatorsNumber { get; set; } = false;
        [JsonProperty("total_stake")]
        public bool OrderByTotalStake { get; set; } = false;
        [JsonProperty("self_stake")]
        public bool OrderBySelfStake { get; set; } = false;
        [JsonProperty("network_share")]
        public bool OrderByNetworkShare { get; set; } = false;


        [JsonProperty("sort_type")]
        public SortType SortType { get; set; } = SortType.Descending;
    }
}