using CSPR.Cloud.Net.Parameters.Filtering.Ft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.Sorting.Ft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Ft
{
    public class FTRateRequestParameters : Paginated
    {
        public FTRateFilterParameters FilterParameters { get; set; }
        public FTRateSortingParameters SortingParameters { get; set; }

        public FTRateRequestParameters()
        {
            FilterParameters = new FTRateFilterParameters();
            SortingParameters = new FTRateSortingParameters();
        }
    }
}
