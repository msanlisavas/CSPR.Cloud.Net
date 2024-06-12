using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Deploy
{
    public class DeployExecutionTypesData
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
