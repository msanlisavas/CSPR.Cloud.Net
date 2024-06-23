using CSPR.Cloud.Net.Parameters.Filtering.Nft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Nft;
using CSPR.Cloud.Net.Parameters.Sorting.Nft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Nft
{
    public class NFTAccountRequestParameters : Paginated
    {
        public NFTOptionalParameters OptionalParameters { get; set; }
        public NFTAccountFilterParameters FilterParameters { get; set; }
        public NFTAccountSortingParameters SortingParameters { get; set; }
        public NFTAccountRequestParameters()
        {
            OptionalParameters = new NFTOptionalParameters();
            FilterParameters = new NFTAccountFilterParameters();
            SortingParameters = new NFTAccountSortingParameters();
        }
    }
}
