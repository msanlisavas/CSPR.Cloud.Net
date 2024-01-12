using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Abstract
{
    public class Response<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
