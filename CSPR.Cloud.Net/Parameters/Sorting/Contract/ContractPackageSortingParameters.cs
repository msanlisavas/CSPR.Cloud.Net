using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Contract
{
    /// <summary>
    /// Base Timestamp Sorting Parameter
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/sorting">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class ContractPackageSortingParameters : BaseSortingParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether to order by timestamp. Set it to true to sort by timestamp.
        /// </summary>
        [JsonProperty("timestamp")]
        public bool OrderByTimestamp { get; set; } = false;

    }
}
