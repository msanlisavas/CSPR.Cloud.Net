using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.AccountInfo
{
    public class LocationData
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
