using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Deploy
{
    public class DeployOptionalParameters
    {
        [JsonProperty("account_info")]
        public bool AccountInfo { get; set; } = false;
        [JsonProperty("centralized_account_info")]
        public bool CentralizedAccountInfo { get; set; } = false;
        [JsonProperty("contract_package")]
        public bool ContractPackage { get; set; } = false;
        [JsonProperty("contract")]
        public bool Contract { get; set; } = false;
        [JsonProperty("contract_entrypoint")]
        public bool ContractEntrypoint { get; set; } = false;
        [JsonProperty("rate")]
        public int Rate { get; set; } = 0;
        [JsonProperty("transfers")]
        public bool Transfers { get; set; } = false;
        [JsonProperty("nft_token_actions")]
        public bool NFTTokenActions { get; set; } = false;
        [JsonProperty("ft_token_actions")]
        public bool FTTokenActions { get; set; } = false;

    }
}
