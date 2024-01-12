using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Objects.Abstract
{
    public class PaginatedResponse<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }

        [JsonProperty("item_count")]
        public int ItemCount { get; set; }

        [JsonProperty("page_count")]
        public int PageCount { get; set; }
    }
}
