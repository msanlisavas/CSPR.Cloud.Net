using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Bidder
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class BidderFilterParameters
    {
        /// <summary>
        /// Filters by EraId
        /// </summary>
        [JsonProperty("era_id")]
        public string EraId { get; set; }
    }
}
