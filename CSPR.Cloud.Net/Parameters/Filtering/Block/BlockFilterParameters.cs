using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Block
{
    public class BlockFilterParameters
    {
        [JsonProperty("proposer_public_key")]
        public string ProposerPublicKey { get; set; } = string.Empty;
    }
}
