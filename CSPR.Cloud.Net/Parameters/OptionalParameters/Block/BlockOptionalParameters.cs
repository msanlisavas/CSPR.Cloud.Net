using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Block
{
    public class BlockOptionalParameters
    {
        [JsonProperty("proposer_account_info")]
        public bool ProposerAccountInfo { get; set; } = false;
    }
}
