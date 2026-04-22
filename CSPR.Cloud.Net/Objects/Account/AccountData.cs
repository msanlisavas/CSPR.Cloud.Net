using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Account
{
    /// <summary>
    /// The Account entity represents the account observed in the network activity.
    /// For example, when the corresponding public key was passed as a deploy argument.
    /// Because of that, an Account may not necessarily have a purse and on-chain balance.
    /// <para>
    /// All balance fields are typed as <see cref="string"/> because the API emits them as JSON
    /// strings to avoid uint64 overflow in clients that don't support 64-bit unsigned integers
    /// (v2.4.3+).
    /// </para>
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
        /// The main purse URef of the account in the uref-dead...beef-007 format.
        /// </summary>
        [JsonProperty("main_purse_uref")]
        public string MainPurseUref { get; set; }

        /// <summary>
        /// Liquid balance of the account main purse in motes.
        /// </summary>
        [JsonProperty("balance")]
        public string Balance { get; set; }

        /// <summary>
        /// The auction status of the account.
        /// </summary>
        [JsonProperty("auction_status")]
        public string AuctionStatus { get; set; }

        /// <summary>
        /// The delegated balance of the account, in motes.
        /// </summary>
        [JsonProperty("delegated_balance")]
        public string DelegatedBalance { get; set; }

        /// <summary>
        /// The undelegated balance of the account, in motes — funds that will return to the main purse after the lockup period ends.
        /// </summary>
        [JsonProperty("undelegated_balance")]
        public string UndelegatedBalance { get; set; }

        /// <summary>
        /// The staked balance of the account, in motes.
        /// </summary>
        [JsonProperty("staked_balance")]
        public string StakedBalance { get; set; }

        /// <summary>
        /// Funds currently being undelegated and awaiting the lockup release, in motes.
        /// </summary>
        [JsonProperty("undelegating_balance")]
        public string UndelegatingBalance { get; set; }

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

        /// <summary>
        /// The account's registered CSPR.name when the <c>cspr_name</c> includer is requested (v2.1.0+).
        /// </summary>
        [JsonProperty("cspr_name")]
        public string CsprName { get; set; }

        /// <summary>
        /// Account rank within the current listing when the <c>rank</c> includer is requested.
        /// </summary>
        [JsonProperty("rank")]
        public uint? Rank { get; set; }
    }
}
