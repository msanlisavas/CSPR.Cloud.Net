using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Objects.AccountInfo
{
    public class AccountInfoData
    {
        [JsonProperty("account_hash")]
        public string AccountHash { get; set; }

        [JsonProperty("created")]
        public DateTime? Created { get; set; }

        [JsonProperty("deploy_hash")]
        public string DeployHash { get; set; }

        [JsonProperty("info")]
        public InfoData Info { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("updated")]
        public DateTime? Updated { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("verified_account_hashes")]
        public List<string> VerifiedAccountHashes { get; set; }
    }
}
