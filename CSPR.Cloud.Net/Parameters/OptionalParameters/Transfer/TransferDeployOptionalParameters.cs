using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Transfer
{
    /// <summary>
    /// Represents optional parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/optional-properties">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class TransferDeployOptionalParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include the deploy caller's public key represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("initiator_public_key")]
        public bool InitiatorPublicKey { get; set; } = false; // Deploy caller public key represented as a hexadecimal string

        /// <summary>
        /// Gets or sets a value indicating whether to include the transfer recipient's public key represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("to_public_key")]
        public bool ToPublicKey { get; set; } = false; // Transfer recipient public key represented as a hexadecimal string

        /// <summary>
        /// Gets or sets a value indicating whether to include the public key of the account that owns the transfer source purse, represented as a hexadecimal string. Null when the sender purse is not owned by an account.
        /// </summary>
        [JsonProperty("from_purse_public_key")]
        public bool FromPursePublicKey { get; set; } = false; // Public key of the account that owns the transfer source purse represented as a hexadecimal string. null when the sender purse is not owned by an account

        /// <summary>
        /// Gets or sets a value indicating whether to include the public key of the account that owns the transfer target purse, represented as a hexadecimal string. Null when the target purse is not owned by an account.
        /// </summary>
        [JsonProperty("to_purse_public_key")]
        public bool ToPursePublicKey { get; set; } = false; // Public key of the account that owns the transfer target purse represented as a hexadecimal string. null when the target purse is not owned by an account

        /// <summary>
        /// Gets or sets a value indicating whether to include the transfer recipient's account info.
        /// </summary>
        [JsonProperty("to_account_info")]
        public bool ToAccountInfo { get; set; } = false; // Transfer recipient account info

        /// <summary>
        /// Gets or sets a value indicating whether to include the transfer recipient's centralized account info.
        /// </summary>
        [JsonProperty("to_centralized_account_info")]
        public bool ToCentralizedAccountInfo { get; set; } = false; // Transfer recipient centralized account info

        /// <summary>
        /// Gets or sets a value indicating whether to include the account info of the account that owns the transfer source purse. Null when the sender purse is not owned by an account.
        /// </summary>
        [JsonProperty("from_purse_account_info")]
        public bool FromPurseAccountInfo { get; set; } = false; // Account info of the account that owns the transfer source purse. null when the sender purse is not owned by an account

        /// <summary>
        /// Gets or sets a value indicating whether to include the centralized account info of the account that owns the transfer source purse. Null when the sender purse is not owned by an account.
        /// </summary>
        [JsonProperty("from_purse_centralized_account_info")]
        public bool FromPurseCentralizedAccountInfo { get; set; } = false; // Centralized account info of the account that owns the transfer source purse. null when the sender purse is not owned by an account

        /// <summary>
        /// Gets or sets a value indicating whether to include the account info of the account that owns the transfer target purse. Null when the target purse is not owned by an account.
        /// </summary>
        [JsonProperty("to_purse_account_info")]
        public bool ToPurseAccountInfo { get; set; } = false; // Account info of the account that owns the transfer target purse. null when the target purse is not owned by an account

        /// <summary>
        /// Gets or sets a value indicating whether to include the centralized account info of the account that owns the transfer target purse. Null when the target purse is not owned by an account.
        /// </summary>
        [JsonProperty("to_purse_centralized_account_info")]
        public bool ToPurseCentralizedAccountInfo { get; set; } = false; // Centralized account info of the account that owns the transfer target purse. null when the target purse is not owned by an account

        /// <summary>
        /// Gets or sets the rate that was relevant at the moment when the last block was proposed.
        /// To include the USD to CSPR rate, pass the USD currency ID (1) as a parameter to the rate function.
        /// For more details, see <see href="https://docs.cspr.cloud/documentation/overview/optional-properties#functions">Including CSPR rates</see>.
        /// </summary>
        [JsonProperty("rate")]
        public int? Rate { get; set; } = 0;

    }
}
