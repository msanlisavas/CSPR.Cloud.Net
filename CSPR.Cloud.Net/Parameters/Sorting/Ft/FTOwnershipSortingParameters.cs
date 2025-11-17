using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Ft
{
    /// <summary>
    /// Represents sorting parameters for fungible token ownership.
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/sorting">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class FTOwnershipSortingParameters : BaseSortingParameters
    {
        /// <summary>
        /// Set it true if you want to sort by balance.
        /// </summary>
        [JsonProperty("balance")]
        public bool OrderByBalance { get; set; } = false;
    }
}
