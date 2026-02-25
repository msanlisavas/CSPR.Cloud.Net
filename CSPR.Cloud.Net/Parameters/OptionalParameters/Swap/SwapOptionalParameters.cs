using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Swap
{
    public class SwapOptionalParameters
    {
        [JsonProperty("sender_public_key")]
        public bool SenderPublicKey { get; set; } = false;

        [JsonProperty("sender_account_info")]
        public bool SenderAccountInfo { get; set; } = false;

        [JsonProperty("sender_centralized_account_info")]
        public bool SenderCentralizedAccountInfo { get; set; } = false;

        [JsonProperty("pair_contract_package")]
        public bool PairContractPackage { get; set; } = false;

        [JsonProperty("token0_contract_package")]
        public bool Token0ContractPackage { get; set; } = false;

        [JsonProperty("token1_contract_package")]
        public bool Token1ContractPackage { get; set; } = false;

        [JsonProperty("token0_ft_rate")]
        public int? Token0FtRate { get; set; } = 0;

        [JsonProperty("token1_ft_rate")]
        public int? Token1FtRate { get; set; } = 0;
    }
}
