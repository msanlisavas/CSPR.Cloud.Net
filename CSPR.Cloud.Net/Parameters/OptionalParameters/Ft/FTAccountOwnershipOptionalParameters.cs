using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Ft
{
    public class FTAccountOwnershipOptionalParameters
    {
        [JsonProperty("contract_package")]
        public bool ContractPackage { get; set; }
    }
}
