using CSPR.Cloud.Net.Parameters.Filtering.Bidder;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Bidder;
using CSPR.Cloud.Net.Parameters.Sorting.Bidder;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Bidder
{
    /// <summary>
    /// Represents request parameters for retrieving bidders with pagination, filtering, sorting, and optional parameters.
    /// </summary>
    public class BiddersRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public BiddersFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public BidderOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public BiddersSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BiddersRequestParameters"/> class.
        /// </summary>
        public BiddersRequestParameters()
        {
            OptionalParameters = new BidderOptionalParameters();
            FilterParameters = new BiddersFilterParameters();
            SortingParameters = new BiddersSortingParameters();
        }
    }

}
