using CSPR.Cloud.Net.Parameters.Filtering.Validator;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.Sorting.Validator;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Validator
{
    public class ValidatorHistoricalPerformanceRequestParameters : Paginated
    {
        public ValidatorHistoricalPerformanceFilterParameters FilterParameters { get; set; }
        public ValidatorHistoricalPerformanceSortingParameters SortingParameters { get; set; }
        public ValidatorHistoricalPerformanceRequestParameters()
        {
            FilterParameters = new ValidatorHistoricalPerformanceFilterParameters();
            SortingParameters = new ValidatorHistoricalPerformanceSortingParameters();

        }
    }
}
