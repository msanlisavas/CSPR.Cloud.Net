using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Socket
{
    /// <summary>
    /// Account balance update emitted on the account-balances stream.
    /// For more information, see <see href="https://docs.cspr.cloud/streaming-api/account-balance">CSPR Cloud Streaming API documentation</see>.
    /// </summary>
    public class AccountBalanceStreamData
    {
        /// <summary>
        /// Account hash represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("account_hash")]
        public string AccountHash { get; set; }

        /// <summary>
        /// Available balance in motes.
        /// </summary>
        [JsonProperty("liquid_balance")]
        public string LiquidBalance { get; set; }

        /// <summary>
        /// Delegated balance in motes.
        /// </summary>
        [JsonProperty("staked_balance")]
        public string StakedBalance { get; set; }

        /// <summary>
        /// Balance that is currently being undelegated, in motes.
        /// </summary>
        [JsonProperty("undelegating_balance")]
        public string UndelegatingBalance { get; set; }
    }
}
