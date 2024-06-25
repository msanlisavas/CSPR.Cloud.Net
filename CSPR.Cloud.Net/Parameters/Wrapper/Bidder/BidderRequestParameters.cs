using CSPR.Cloud.Net.Parameters.Filtering.Bidder;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Bidder;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Bidder
{
    /// <summary>
    /// Represents request parameters for retrieving bidder information with filtering and optional parameters.
    /// </summary>
    public class BidderRequestParameters
    {
        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public BidderFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public BidderOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BidderRequestParameters"/> class.
        /// </summary>
        public BidderRequestParameters()
        {
            OptionalParameters = new BidderOptionalParameters();
            FilterParameters = new BidderFilterParameters();
        }
    }

}
