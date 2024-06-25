using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Filtering.Account
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class AccountInfosFilterParameters
    {
        /// <summary>
        /// List of account hashes
        /// </summary>
        [JsonProperty("account_hash")]
        public List<string> AccountHashes { get; set; } // Comma-seperated list of account hashes
    }
}
