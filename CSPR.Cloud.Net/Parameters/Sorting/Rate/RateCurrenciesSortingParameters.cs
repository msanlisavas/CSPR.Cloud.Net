using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Rate
{
    /// <summary>
    /// Base Timestamp Sorting Parameter
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/sorting">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class RateCurrenciesSortingParameters : BaseSortingParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether to order by ID. Set it to true to sort by ID.
        /// </summary>
        [JsonProperty("id")]
        public bool OrderById { get; set; } = false;

    }
}
