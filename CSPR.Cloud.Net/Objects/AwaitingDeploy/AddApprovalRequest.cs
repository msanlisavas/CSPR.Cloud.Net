using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.AwaitingDeploy
{
    public class AddApprovalRequest
    {
        [JsonProperty("signer")]
        public string Signer { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}
