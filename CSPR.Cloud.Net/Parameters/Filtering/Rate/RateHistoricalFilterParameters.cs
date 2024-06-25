using Newtonsoft.Json;
using System;

namespace CSPR.Cloud.Net.Parameters.Filtering.Rate
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class RateHistoricalFilterParameters
    {
        /// <summary>
        /// Filters by the starting date and time.
        /// </summary>
        [JsonProperty("from")]
        public DateTime? From { get; set; }

        /// <summary>
        /// Filters by the ending date and time.
        /// </summary>
        [JsonProperty("to")]
        public DateTime? To { get; set; }

    }
}
