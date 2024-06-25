using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Objects.AccountInfo
{
    /// <summary>
    /// The AccountInfo entity represents a record of an account along with related metadata in the context of the Casper Account Info Standard.
    /// This entity is parsed from the Casper Account Info Contract and stores a denormalized representation of the data from this contract.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/account-info">CSPR Cloud API documentation</see>.
    /// </summary>
    public class AccountInfoData
    {
        /// <summary>
        /// Account hash represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("account_hash")]
        public string AccountHash { get; set; }

        /// <summary>
        /// Account info creation timestamp.
        /// </summary>
        [JsonProperty("created")]
        public DateTime? Created { get; set; }

        /// <summary>
        /// Hash of the deploy that deployed the Casper Account Info Contract to the network represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("deploy_hash")]
        public string DeployHash { get; set; }

        /// <summary>
        /// JSON representation of account info data, related to Casper Account Info Standard.
        /// </summary>
        [JsonProperty("info")]
        public InfoData Info { get; set; }

        /// <summary>
        /// Status describes whether account info is active or not.
        /// </summary>
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Account info last update timestamp.
        /// </summary>
        [JsonProperty("updated")]
        public DateTime? Updated { get; set; }

        /// <summary>
        /// The top-level domain URL for an account information.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// List of verified account hashes represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("verified_account_hashes")]
        public List<string> VerifiedAccountHashes { get; set; }
    }
}
