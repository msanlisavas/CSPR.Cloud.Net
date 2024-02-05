using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Objects.AccountInfo
{
    public class ResourcesData
    {
        [JsonProperty("code_of_conduct_url")]
        public string CodeOfConductUrl { get; set; }

        [JsonProperty("other")]
        public List<object> Other { get; set; }

        [JsonProperty("privacy_policy_url")]
        public string PrivacyPolicyUrl { get; set; }

        [JsonProperty("terms_of_service_url")]
        public string TermsOfServiceUrl { get; set; }
    }
}
