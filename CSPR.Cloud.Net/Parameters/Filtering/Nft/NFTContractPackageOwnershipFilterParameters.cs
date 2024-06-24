using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Nft
{
    public class NFTContractPackageOwnershipFilterParameters
    {
        [JsonProperty("owner_hash")]
        public string OwnerHash { get; set; }
    }
}
