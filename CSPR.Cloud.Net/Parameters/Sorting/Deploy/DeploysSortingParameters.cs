using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Deploy
{
    public class DeploysSortingParameters : BaseSortingParameters
    {
        [JsonProperty("timestamp")]
        public bool OrderByTimestamp { get; set; } = false;


    }
}
