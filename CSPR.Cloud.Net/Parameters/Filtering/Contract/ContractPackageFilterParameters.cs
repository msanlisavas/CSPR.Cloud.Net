using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Contract
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class ContractPackageFilterParameters
    {
        /// <summary>
        /// Filters by Owner Public Key
        /// </summary>
        [JsonProperty("owner_public_key")]
        public string OwnerPublicKey { get; set; }
    }
}
