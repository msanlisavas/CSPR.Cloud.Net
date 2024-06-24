using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Nft
{
    public class NFTContractPackageOwnershipOptionalParameters
    {
        [JsonProperty("contract_package")]
        public bool ContractPackage { get; set; } = false;
        [JsonProperty("owner_public_key")]
        public bool OwnerPublicKey { get; set; } = false;

    }
}
