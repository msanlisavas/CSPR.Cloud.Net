using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Filtering.CentralizedAccountInfo
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class CentralizedAccountInfoFilterParameters
    {
        /// <summary>
        /// Filters by Account Hash
        /// </summary>
        [JsonProperty("account_hash")]
        public List<string> AccountHashes { get; set; } = new List<string>();
    }
}
