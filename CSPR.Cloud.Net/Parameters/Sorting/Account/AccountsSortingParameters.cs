using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Account
{
    /// <summary>
    /// Represents sorting parameters for accounts.
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/sorting">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class AccountsSortingParameters : BaseSortingParameters
    {
        /// <summary>
        /// Set it true if you want to sort by balance.
        /// </summary>
        [JsonProperty("balance")]
        public bool OrderByBalance { get; set; } = false;
        /// <summary>
        /// Set it true if you want to sort by total balance.
        /// </summary>
        [JsonProperty("total_balance")]
        public bool OrderByTotalBalance { get; set; } = false;

    }
}
