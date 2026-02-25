using Newtonsoft.Json.Linq;

namespace CSPR.Cloud.Net.Objects.AwaitingDeploy
{
    public class CreateAwaitingDeployRequest
    {
        public JObject Deploy { get; set; }
    }
}
