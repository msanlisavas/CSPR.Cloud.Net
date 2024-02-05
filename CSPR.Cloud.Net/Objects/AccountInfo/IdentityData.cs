using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.AccountInfo
{
    public class IdentityData
    {
        [JsonProperty("casper_association_kyc_onchain")]
        public string CasperAssociationKycOnchain { get; set; }

        [JsonProperty("casper_association_kyc_url")]
        public string CasperAssociationKycUrl { get; set; }

        [JsonProperty("other")]
        public object Other { get; set; }

        [JsonProperty("ownership_disclosure_url")]
        public string OwnershipDisclosureUrl { get; set; }
    }
}
