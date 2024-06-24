using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Nft
{
    public class NFTAccountOwnershipFilterParameters
    {
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }
    }
}
