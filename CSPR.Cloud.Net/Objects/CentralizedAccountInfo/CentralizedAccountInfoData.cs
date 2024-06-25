using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.CentralizedAccountInfo
{
    /// <summary>
    /// The CentralizedAccountInfo entity provides account information collected by the CSPR.cloud team for the well-known accounts in the Casper ecosystem.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/centralized-account-info">CSPR Cloud API documentation</see>.
    /// </summary>
    public class CentralizedAccountInfoData
    {
        /// <summary>
        /// Account hash represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("account_hash")]
        public string AccountHash { get; set; }

        /// <summary>
        /// Account avatar URL.
        /// </summary>
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Account name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Website URL associated with the account.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }

}
