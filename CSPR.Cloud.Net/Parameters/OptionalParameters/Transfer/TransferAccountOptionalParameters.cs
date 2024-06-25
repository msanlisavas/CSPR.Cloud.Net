using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Transfer
{
    public class TransferAccountOptionalParameters
    {
        [JsonProperty("initiator_public_key")]
        public bool InitiatorPublicKey { get; set; } = false; // Deploy caller public key represented as a hexadecimal string

        [JsonProperty("to_public_key")]
        public bool ToPublicKey { get; set; } = false;// Transfer recipient public key represented as a hexadecimal string

        [JsonProperty("from_purse_public_key")]
        public bool FromPursePublicKey { get; set; } = false;// Public key of the account that owns the transfer source purse represented as a hexadecimal string. null when the sender purse is not owned by an account

        [JsonProperty("to_purse_public_key")]
        public bool ToPursePublicKey { get; set; } = false;// Public key of the account that owns the transfer target purse represented as a hexadecimal string. null when the target purse is not owned by an account

        [JsonProperty("to_account_info")]
        public bool ToAccountInfo { get; set; } = false;// Transfer recipient account info

        [JsonProperty("to_centralized_account_info")]
        public bool ToCentralizedAccountInfo { get; set; } = false;// Transfer recipient centralized account info

        [JsonProperty("from_purse_account_info")]
        public bool FromPurseAccountInfo { get; set; } = false;// Account info of the account that owns the transfer source purse. null when the sender purse is not owned by an account

        [JsonProperty("from_purse_centralized_account_info")]
        public bool FromPurseCentralizedAccountInfo { get; set; } = false; // Centralized account info of the account that owns the transfer source purse. null when the sender purse is not owned by an account

        [JsonProperty("to_purse_account_info")]
        public bool ToPurseAccountInfo { get; set; } = false;// Account info of the account that owns the transfer target purse. null when the target purse is not owned by an account

        [JsonProperty("to_purse_centralized_account_info")]
        public bool ToPurseCentralizedAccountInfo { get; set; } = false;// Centralized account info of the account that owns the transfer target purse. null when the target purse is not owned by an account

        [JsonProperty("rate")]
        public int Rate { get; set; } = 0;
    }
}
