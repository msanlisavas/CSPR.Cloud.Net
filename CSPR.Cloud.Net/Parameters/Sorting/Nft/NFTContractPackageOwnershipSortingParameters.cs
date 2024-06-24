using CSPR.Cloud.Net.Parameters.Sorting.Abstract;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.Sorting.Nft
{
    public class NFTContractPackageOwnershipSortingParameters : BaseSortingParameters
    {
        [JsonProperty("tokens_number")]
        public bool OrderByTokensNumber { get; set; } = false;
    }
}
