using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Contract
{
    public class ContractOptionalParameters
    {
        [JsonProperty("contract_package")]
        public bool ContractPackage { get; set; } = false;
    }
}
