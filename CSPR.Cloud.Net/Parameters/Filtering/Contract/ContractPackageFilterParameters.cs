using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Contract
{
    public class ContractPackageFilterParameters
    {
        [JsonProperty("owner_public_key")]
        public string OwnerPublicKey { get; set; }
    }
}
