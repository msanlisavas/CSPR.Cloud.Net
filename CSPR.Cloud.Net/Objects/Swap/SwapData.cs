using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using CSPR.Cloud.Net.Objects.Contract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Swap
{
    public class SwapData
    {
        [JsonProperty("pair_contract_package_hash")]
        public string PairContractPackageHash { get; set; }

        [JsonProperty("transaction_hash")]
        public string TransactionHash { get; set; }

        [JsonProperty("transform_id")]
        public uint? TransformId { get; set; }

        [JsonProperty("block_height")]
        public ulong? BlockHeight { get; set; }

        [JsonProperty("sender_hash")]
        public string SenderHash { get; set; }

        [JsonProperty("dex_id")]
        public int? DexId { get; set; }

        [JsonProperty("token0_contract_package_hash")]
        public string Token0ContractPackageHash { get; set; }

        [JsonProperty("token1_contract_package_hash")]
        public string Token1ContractPackageHash { get; set; }

        [JsonProperty("decimals0")]
        public int? Decimals0 { get; set; }

        [JsonProperty("decimals1")]
        public int? Decimals1 { get; set; }

        [JsonProperty("amount0_in")]
        public string Amount0In { get; set; }

        [JsonProperty("amount1_in")]
        public string Amount1In { get; set; }

        [JsonProperty("amount0_out")]
        public string Amount0Out { get; set; }

        [JsonProperty("amount1_out")]
        public string Amount1Out { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("sender_public_key")]
        public string SenderPublicKey { get; set; }

        [JsonProperty("sender_account_info")]
        public AccountInfoData SenderAccountInfo { get; set; }

        [JsonProperty("sender_centralized_account_info")]
        public CentralizedAccountInfoData SenderCentralizedAccountInfo { get; set; }

        [JsonProperty("pair_contract_package")]
        public ContractPackageData PairContractPackage { get; set; }

        [JsonProperty("token0_contract_package")]
        public ContractPackageData Token0ContractPackage { get; set; }

        [JsonProperty("token1_contract_package")]
        public ContractPackageData Token1ContractPackage { get; set; }

        [JsonProperty("token0_ft_rate")]
        public float? Token0FtRate { get; set; }

        [JsonProperty("token1_ft_rate")]
        public float? Token1FtRate { get; set; }
    }
}
