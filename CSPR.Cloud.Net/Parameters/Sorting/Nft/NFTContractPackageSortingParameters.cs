using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Nft
{
    public class NFTContractPackageSortingParameters : TimestampSortingParameters
    {
        [JsonProperty("token_id")]
        public bool TokenId { get; set; } = false;
    }
}
