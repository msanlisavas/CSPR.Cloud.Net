using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Objects.Abstract
{
    public class ListResponse<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}
