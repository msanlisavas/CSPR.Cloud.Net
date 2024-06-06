using CSPR.Cloud.Net.Parameters.Filtering.Bidder;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Bidder;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Bidder
{
    public class BidderRequestParameters
    {
        public BidderFilterParameters FilterParameters { get; set; }
        public BidderOptionalParameters OptionalParameters { get; set; }
        public BidderRequestParameters()
        {
            OptionalParameters = new BidderOptionalParameters();
            FilterParameters = new BidderFilterParameters();
        }
    }
}
