using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Ft
{
    public class FTOwnershipData
    {
        [JsonProperty("balance")]
        public string Balance { get; set; }
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }
        [JsonProperty("owner_hash")]
        public string OwnerHash { get; set; }
    }
}
