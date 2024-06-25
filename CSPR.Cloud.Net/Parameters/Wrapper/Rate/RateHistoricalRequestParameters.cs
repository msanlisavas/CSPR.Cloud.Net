using CSPR.Cloud.Net.Parameters.Filtering.Rate;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.Sorting.Rate;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Rate
{
    /// <summary>
    /// Represents request parameters for retrieving historical rates with pagination, filtering, and sorting options.
    /// </summary>
    public class RateHistoricalRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public RateHistoricalFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public RateHistoricalSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RateHistoricalRequestParameters"/> class.
        /// </summary>
        public RateHistoricalRequestParameters()
        {
            FilterParameters = new RateHistoricalFilterParameters();
            SortingParameters = new RateHistoricalSortingParameters();
        }
    }

}
