using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CSPR.Cloud.Net.Objects.AwaitingDeploy
{
    public class AwaitingDeployData
    {
        [JsonProperty("deploy")]
        public JObject Deploy { get; set; }
    }
}
