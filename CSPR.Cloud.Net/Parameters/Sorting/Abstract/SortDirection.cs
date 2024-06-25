using CSPR.Cloud.Net.Enums;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Abstract
{
    /// <summary>
    /// Core Sorting Parameter. Defines the sort direction. It's inherited by other sorting parameters.
    /// Descending by default.
    /// </summary>
    public class SortDirection
    {
        /// <summary>
        /// Sets the sort type to either ascending or descending.
        /// Default is descending.
        /// </summary>

        [JsonProperty("sort_type")]
        public SortType SortType { get; set; } = SortType.Descending;
    }
}
