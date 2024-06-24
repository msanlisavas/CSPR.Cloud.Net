using CSPR.Cloud.Net.Parameters.Filtering.Rate;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.Sorting.Rate;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Rate
{
    public class RateHistoricalRequestParameters : Paginated
    {
        public RateHistoricalFilterParameters FilterParameters { get; set; }
        public RateHistoricalSortingParameters SortingParameters { get; set; }
        public RateHistoricalRequestParameters()
        {
            FilterParameters = new RateHistoricalFilterParameters();
            SortingParameters = new RateHistoricalSortingParameters();
        }
    }
}
