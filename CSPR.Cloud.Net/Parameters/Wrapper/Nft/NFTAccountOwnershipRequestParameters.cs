using CSPR.Cloud.Net.Parameters.Filtering.Nft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Nft;
using CSPR.Cloud.Net.Parameters.Sorting.Nft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Nft
{
    public class NFTAccountOwnershipRequestParameters : Paginated
    {
        public NFTAccountOwnershipOptionalParameters OptionalParameters { get; set; }
        public NFTAccountOwnershipFilterParameters FilterParameters { get; set; }
        public NFTAccountOwnershipSortingParameters SortingParameters { get; set; }
        public NFTAccountOwnershipRequestParameters()
        {
            OptionalParameters = new NFTAccountOwnershipOptionalParameters();
            FilterParameters = new NFTAccountOwnershipFilterParameters();
            SortingParameters = new NFTAccountOwnershipSortingParameters();
        }
    }
}
