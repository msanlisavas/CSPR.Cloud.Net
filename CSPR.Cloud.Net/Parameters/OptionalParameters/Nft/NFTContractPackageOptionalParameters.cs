using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Nft
{
    public class NFTContractPackageOptionalParameters
    {
        [JsonProperty("owner_public_key")]
        public bool OwnerPublicKey { get; set; } = false;
    }
}
