using CSPR.Cloud.Net.Enums;
using CSPR.Cloud.Net.Objects.Contract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Nft
{
    public class NFTTokenOwnershipData
    {
        [JsonProperty("owner_hash")]
        public string OwnerHash { get; set; }
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }
        [JsonProperty("owner_type")]
        public OwnerType? OwnerType { get; set; }
        [JsonProperty("tokens_number")]
        public uint? TokensNumber { get; set; }
        [JsonProperty("contract_package")]
        public ContractPackageData ContractPackage { get; set; }
        [JsonProperty("owner_public_key")]
        public string OwnerPublicKey { get; set; }
    }
}
