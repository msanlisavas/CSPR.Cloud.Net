using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Nft
{
    /// <summary>
    /// Base Timestamp Sorting Parameter
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/sorting">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class NFTContractPackageOwnershipSortingParameters : BaseSortingParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether to order by the number of tokens. Set it to true to sort by the number of tokens.
        /// </summary>
        [JsonProperty("tokens_number")]
        public bool OrderByTokensNumber { get; set; } = false;

    }
}
