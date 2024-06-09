using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Delegate
{
    public class DelegationData
    {
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
        [JsonProperty("validator_public_key")]
        public string ValidatorPublicKey { get; set; }
        [JsonProperty("stake")]
        public string Stake { get; set; }
        [JsonProperty("bonding_purse")]
        public string BondingPurse { get; set; }
    }
}
