using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.Sorting.Rate;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Rate
{
    /// <summary>
    /// Represents request parameters for retrieving rate currencies with pagination and sorting options.
    /// </summary>
    public class RateCurrenciesRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public RateCurrenciesSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RateCurrenciesRequestParameters"/> class.
        /// </summary>
        public RateCurrenciesRequestParameters()
        {
            SortingParameters = new RateCurrenciesSortingParameters();
        }
    }

}
