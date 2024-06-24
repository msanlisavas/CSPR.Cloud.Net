using CSPR.Cloud.Net.Parameters.Filtering.Nft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Nft;
using CSPR.Cloud.Net.Parameters.Sorting.Nft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Nft
{
    public class NFTAccountActionsRequestParameters : Paginated
    {
        public NFTAccountActionsOptionalParameters OptionalParameters { get; set; }
        public NFTAccountActionsFilterParameters FilterParameters { get; set; }
        public NFTAccountActionsSortingParameters SortingParameters { get; set; }
        public NFTAccountActionsRequestParameters()
        {
            OptionalParameters = new NFTAccountActionsOptionalParameters();
            FilterParameters = new NFTAccountActionsFilterParameters();
            SortingParameters = new NFTAccountActionsSortingParameters();
        }
    }
}
