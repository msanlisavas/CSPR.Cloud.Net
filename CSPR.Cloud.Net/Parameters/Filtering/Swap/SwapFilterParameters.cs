using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Filtering.Swap
{
    public class SwapFilterParameters
    {
        [JsonProperty("token_contract_package_hash")]
        public List<string> TokenContractPackageHashes { get; set; } = new List<string>();

        [JsonProperty("pair_contract_package_hash")]
        public List<string> PairContractPackageHashes { get; set; } = new List<string>();

        [JsonProperty("sender_account_hash")]
        public List<string> SenderAccountHashes { get; set; } = new List<string>();

        [JsonProperty("dex_id")]
        public List<string> DexIds { get; set; } = new List<string>();
    }
}
