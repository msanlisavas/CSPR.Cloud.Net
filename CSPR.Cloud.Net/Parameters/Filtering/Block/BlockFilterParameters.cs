using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Block
{
    /// <summary>
    /// Represents filtering parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/filtering">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class BlockFilterParameters
    {
        /// <summary>
        /// Filters by Proposer Public Key
        /// </summary>
        [JsonProperty("proposer_public_key")]
        public string ProposerPublicKey { get; set; }
    }
}
