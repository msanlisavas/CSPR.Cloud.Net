using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Objects.AccountInfo
{
    public class OwnerData
    {
        [JsonProperty("affiliated_accounts")]
        public List<AffiliatedAccountData> AffiliatedAccounts { get; set; }

        [JsonProperty("branding")]
        public BrandingData Branding { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("identity")]
        public IdentityData Identity { get; set; }

        [JsonProperty("location")]
        public LocationData Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("resources")]
        public ResourcesData Resources { get; set; }

        [JsonProperty("social")]
        public SocialData Social { get; set; }

        [JsonProperty("type")]
        public List<string> Type { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }
    }
}
