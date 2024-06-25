using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Validator
{
    public class ValidatorData
    {
        [JsonProperty("rank")]
        public uint? Rank { get; set; } // Projected validator rank based on total stake amount

        [JsonProperty("era_id")]
        public uint? EraId { get; set; } // Current era validator identifier

        [JsonProperty("public_key")]
        public string PublicKey { get; set; } // Validator public key represented as a hexadecimal string

        [JsonProperty("is_active")]
        public bool IsActive { get; set; } // Describes whether the validator is active or not

        [JsonProperty("fee")]
        public float? Fee { get; set; } // Percentage of the rewards the validator retains for its services

        [JsonProperty("delegators_number")]
        public ulong? DelegatorsNumber { get; set; } // Number of delegators staked to validator

        [JsonProperty("self_stake")]
        public ulong? SelfStake { get; set; } // Validator self-stake calculated as a sum of the bidder stake and the stakes of the affiliated accounts provided via the Casper Account Info Standard

        [JsonProperty("delegators_stake")]
        public ulong? DelegatorsStake { get; set; } // Cumulative stake of all delegators

        [JsonProperty("total_stake")]
        public ulong? TotalStake { get; set; } // Total validator stake. The sum of the self-stake and the delegator stakes

        [JsonProperty("self_share")]
        public float? SelfShare { get; set; } // Percentage of the validator's self-stake to its total stake

        [JsonProperty("network_share")]
        public float? NetworkShare { get; set; } // Percentage of the validator's total stake to the total amount staked on the network
        [JsonProperty("account_info")]
        public AccountInfoData AccountInfo { get; set; } // Validator account information
        [JsonProperty("centralized_account_info")]
        public CentralizedAccountInfoData CentralizedAccountInfo { get; set; } // Validator account information
        [JsonProperty("average_performance")]
        public ValidatorPerformanceData AveragePerformance { get; set; } // Average performance of the validator in the last 30 days

    }
}
