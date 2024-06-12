using CSPR.Cloud.Net.Enums;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Abstract
{
    public class SortDirection
    {
        [JsonProperty("sort_type")]
        public SortType SortType { get; set; } = SortType.Descending;
    }
}
