using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Filtering.Account
{
    public class AccountInfosFilterParameters
    {
        [JsonProperty("account_hash")]
        public List<string> AccountHashes { get; set; } // Comma-seperated list of account hashes
    }
}
