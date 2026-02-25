using CSPR.Cloud.Net.Parameters.Filtering.Ft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.Sorting.Ft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Ft
{
    public class FTDailyRateRequestParameters : Paginated
    {
        public FTRateFilterParameters FilterParameters { get; set; }
        public FTDailyRateSortingParameters SortingParameters { get; set; }

        public FTDailyRateRequestParameters()
        {
            FilterParameters = new FTRateFilterParameters();
            SortingParameters = new FTDailyRateSortingParameters();
        }
    }
}
