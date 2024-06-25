using CSPR.Cloud.Net.Parameters.Filtering.Validator;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.Sorting.Validator;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Validator
{
    public class ValidatorHistoricalAveragePerformanceRequestParameters : Paginated
    {
        public ValidatorHistoricalAveragePerformanceFilterParameters FilterParameters { get; set; }
        public ValidatorHistoricalAveragePerformanceSortingParameters SortingParameters { get; set; }
        public ValidatorHistoricalAveragePerformanceRequestParameters()
        {
            FilterParameters = new ValidatorHistoricalAveragePerformanceFilterParameters();
            SortingParameters = new ValidatorHistoricalAveragePerformanceSortingParameters();

        }
    }
}
