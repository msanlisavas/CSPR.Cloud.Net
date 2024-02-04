using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Account
{
    public class AccountsQueryParameters
    {
        [JsonProperty("account_hash")]
        public List<string> AccountHashes { get; set; } = new List<string>();
    }
}
