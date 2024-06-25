using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Nft
{
    /// <summary>
    /// Base Timestamp Sorting Parameter
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/sorting">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class NFTContractPackageSortingParameters : TimestampSortingParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether to order by token ID. Set it to true to sort by token ID.
        /// </summary>
        [JsonProperty("token_id")]
        public bool OrderByTokenId { get; set; } = false;

    }
}
