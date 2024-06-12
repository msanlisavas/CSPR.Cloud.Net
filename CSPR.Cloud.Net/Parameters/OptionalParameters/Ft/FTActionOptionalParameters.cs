using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Ft
{
    public class FTActionOptionalParameters
    {
        [JsonProperty("deploy")]
        public bool Deploy { get; set; } = false;
        [JsonProperty("contract_package")]
        public bool ContractPackage { get; set; } = false;
        [JsonProperty("from_public_key")]
        public bool FromPublicKey { get; set; } = false;
        [JsonProperty("to_public_key")]
        public bool ToPublicKey { get; set; } = false;
    }
}
