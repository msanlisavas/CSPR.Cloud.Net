using CSPR.Cloud.Net.Parameters.Filtering.Validator;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.Sorting.Validator;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Validator
{
    /// <summary>
    /// Represents request parameters for retrieving validators' historical average performance with pagination, filtering, and sorting options.
    /// </summary>
    public class ValidatorsHistoricalAveragePerformanceRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public ValidatorsHistoricalAveragePerformanceFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public ValidatorsHistoricalAveragePerformanceSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorsHistoricalAveragePerformanceRequestParameters"/> class.
        /// </summary>
        public ValidatorsHistoricalAveragePerformanceRequestParameters()
        {
            FilterParameters = new ValidatorsHistoricalAveragePerformanceFilterParameters();
            SortingParameters = new ValidatorsHistoricalAveragePerformanceSortingParameters();
        }
    }
}
