using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Filtering.Validator
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class ValidatorHistoricalPerformanceFilterParameters
    {
        /// <summary>
        /// List of Era Ids
        /// </summary>
        [JsonProperty("era_id")]
        public List<string> EraIds { get; set; }

    }
}
