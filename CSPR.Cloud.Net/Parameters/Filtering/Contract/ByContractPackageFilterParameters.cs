using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Contract
{
    public class ByContractPackageFilterParameters
    {
        [JsonProperty("deploy_hash")]
        public string DeployHash { get; set; }
    }
}
