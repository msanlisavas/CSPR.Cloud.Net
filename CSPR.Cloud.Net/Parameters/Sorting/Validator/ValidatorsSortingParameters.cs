using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Validator
{
    /// <summary>
    /// Base Timestamp Sorting Parameter
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/sorting">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class ValidatorsSortingParameters : BaseSortingParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether to order by rank. Set it to true to sort by rank.
        /// </summary>
        [JsonProperty("rank")]
        public bool OrderByRank { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to order by fee. Set it to true to sort by fee.
        /// </summary>
        [JsonProperty("fee")]
        public bool OrderByFee { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to order by the number of delegators. Set it to true to sort by the number of delegators.
        /// </summary>
        [JsonProperty("delegators_number")]
        public bool OrderByDelegatorsNumber { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to order by total stake. Set it to true to sort by total stake.
        /// </summary>
        [JsonProperty("total_stake")]
        public bool OrderByTotalStake { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to order by self stake. Set it to true to sort by self stake.
        /// </summary>
        [JsonProperty("self_stake")]
        public bool OrderBySelfStake { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to order by network share. Set it to true to sort by network share.
        /// </summary>
        [JsonProperty("network_share")]
        public bool OrderByNetworkShare { get; set; } = false;

    }
}
