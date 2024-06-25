using CSPR.Cloud.Net.Parameters.Filtering.Validator;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.Sorting.Validator;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Validator
{
    /// <summary>
    /// Represents request parameters for retrieving validator historical average performance with pagination, filtering, and sorting options.
    /// </summary>
    public class ValidatorHistoricalAveragePerformanceRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public ValidatorHistoricalAveragePerformanceFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public ValidatorHistoricalAveragePerformanceSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorHistoricalAveragePerformanceRequestParameters"/> class.
        /// </summary>
        public ValidatorHistoricalAveragePerformanceRequestParameters()
        {
            FilterParameters = new ValidatorHistoricalAveragePerformanceFilterParameters();
            SortingParameters = new ValidatorHistoricalAveragePerformanceSortingParameters();
        }
    }

}
