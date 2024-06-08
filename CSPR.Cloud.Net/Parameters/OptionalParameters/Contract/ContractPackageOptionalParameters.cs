using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Contract
{
    public class ContractPackageOptionalParameters
    {
        [JsonProperty("deploys_number")]
        public int DeploysNumber { get; set; } = 0;

    }
}
