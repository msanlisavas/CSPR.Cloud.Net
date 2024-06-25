using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Account
{
    /// <summary>
    /// The Account entity represents the account observed in the network activity.
    /// For example, when the corresponding public key was passed as a deploy argument.
    /// Because of that, an Account may not necessarily have a purse and on-chain balance.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/account">CSPR Cloud API documentation</see>.
    /// </summary>
    public class AccountData
    {
        /// <summary>
        /// The account public key represented as a hexadecimal string. Primary account identifier.
        /// </summary>
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        /// <summary>
        /// The 32-byte hash of the public key represented as a hexadecimal string. Secondary account identifier.
        /// </summary>
        [JsonProperty("account_hash")]
        public string AccountHash { get; set; }

        /// <summary>
        /// The main purse URef (Universal Reference) of the account in the uref-dead...beef-007 format.
        /// </summary>
        [JsonProperty("main_purse_uref")]
        public string MainPurseUref { get; set; }

        /// <summary>
        /// The balance of the account main purse in motes.
        /// </summary>
        [JsonProperty("balance")]
        public ulong? Balance { get; set; }

        /// <summary>
        /// The auction status of the account.
        /// </summary>
        [JsonProperty("auction_status")]
        public string AuctionStatus { get; set; }

        /// <summary>
        /// The delegated balance of the account.
        /// </summary>
        [JsonProperty("delegated_balance")]
        public ulong? DelegatedBalance { get; set; }

        /// <summary>
        /// The undelegated balance of the account.
        /// </summary>
        [JsonProperty("undelegated_balance")]
        public ulong? UndelegatedBalance { get; set; }

        /// <summary>
        /// The staked balance of the account.
        /// </summary>
        [JsonProperty("staked_balance")]
        public ulong? StakedBalance { get; set; }

        /// <summary>
        /// Additional account information.
        /// </summary>
        [JsonProperty("account_info")]
        public AccountInfoData AccountInfo { get; set; }

        /// <summary>
        /// Centralized account information.
        /// </summary>
        [JsonProperty("centralized_account_info")]
        public CentralizedAccountInfoData CentralizedAccountInfo { get; set; }
    }
}
