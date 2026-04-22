using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Ft
{
    /// <summary>
    /// Filtering parameters for the account-scoped FT ownership endpoint (<c>/accounts/{pk}/ft-token-ownership</c>).
    /// Supports filtering by contract package hash (v2.6.0+).
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class FTAccountOwnershipFilterParameters
    {
        /// <summary>
        /// Narrows results to a single FT contract package.
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }
    }
}
