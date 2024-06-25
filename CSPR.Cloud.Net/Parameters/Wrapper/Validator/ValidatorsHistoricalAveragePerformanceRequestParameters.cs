using CSPR.Cloud.Net.Parameters.Filtering.Validator;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.Sorting.Validator;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Validator
{
    public class ValidatorsHistoricalAveragePerformanceRequestParameters : Paginated
    {
        public ValidatorsHistoricalAveragePerformanceFilterParameters FilterParameters { get; set; }
        public ValidatorsHistoricalAveragePerformanceSortingParameters SortingParameters { get; set; }
        public ValidatorsHistoricalAveragePerformanceRequestParameters()
        {
            FilterParameters = new ValidatorsHistoricalAveragePerformanceFilterParameters();
            SortingParameters = new ValidatorsHistoricalAveragePerformanceSortingParameters();

        }
    }
}
