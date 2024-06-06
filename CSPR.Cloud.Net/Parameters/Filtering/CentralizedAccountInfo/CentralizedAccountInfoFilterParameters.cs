using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Filtering.CentralizedAccountInfo
{
    public class CentralizedAccountInfoFilterParameters
    {
        [JsonProperty("account_hash")]
        public List<string> AccountHashes { get; set; } = new List<string>();
    }
}
