using CSPR.Cloud.Net.Parameters.Filtering.Bidder;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Bidder;
using CSPR.Cloud.Net.Parameters.Sorting.Bidder;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Bidder
{
    public class BiddersRequestParameters : Paginated
    {
        public BiddersFilterParameters FilterParameters { get; set; }
        public BidderOptionalParameters OptionalParameters { get; set; }
        public BiddersSortingParameters SortingParameters { get; set; }
        public BiddersRequestParameters()
        {
            OptionalParameters = new BidderOptionalParameters();
            FilterParameters = new BiddersFilterParameters();
            SortingParameters = new BiddersSortingParameters();
        }
    }
}
