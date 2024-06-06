using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Filtering.Bidder
{
    public class BidderFilterParameters
    {
        [JsonProperty("era_id")]
        public string EraId { get; set; }
    }
}
