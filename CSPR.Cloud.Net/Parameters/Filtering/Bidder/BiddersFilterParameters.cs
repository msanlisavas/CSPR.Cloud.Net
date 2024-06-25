using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Filtering.Bidder
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class BiddersFilterParameters
    {
        /// <summary>
        /// Filters by EraId
        /// </summary>
        [JsonProperty("era_id")]
        public string EraId { get; set; }
        /// <summary>
        /// Filters by IsActive
        /// </summary>
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
        /// <summary>
        /// Filters by Lists of PublicKey
        /// </summary>
        [JsonProperty("public_key")]
        public List<string> PublicKey { get; set; } = new List<string>();
    }
}
