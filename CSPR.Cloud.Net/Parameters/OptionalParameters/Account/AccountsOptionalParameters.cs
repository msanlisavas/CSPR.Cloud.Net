using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Account
{
    /// <summary>
    /// Represents optional parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/optional-properties">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class AccountsOptionalParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include auction status. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("auction_status")]
        public bool AuctionStatus { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include delegated balance. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("delegated_balance")]
        public bool DelegatedBalance { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include undelegating balance. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("undelegating_balance")]
        public bool UndelegatingBalance { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include staked balance. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("staked_balance")]
        public bool StakedBalance { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include account info. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("account_info")]
        public bool AccountInfo { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include centralized account info. Set it to true to include it in the response.
        /// </summary>
        [JsonProperty("centralized_account_info")]
        public bool CentralizedAccountInfo { get; set; } = false;
    }

}
