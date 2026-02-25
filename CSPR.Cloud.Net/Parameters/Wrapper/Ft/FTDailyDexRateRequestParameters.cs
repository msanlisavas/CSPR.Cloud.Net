using CSPR.Cloud.Net.Parameters.Filtering.Ft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Ft;
using CSPR.Cloud.Net.Parameters.Sorting.Ft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Ft
{
    public class FTDailyDexRateRequestParameters : Paginated
    {
        public FTDexRateFilterParameters FilterParameters { get; set; }
        public FTDailyRateSortingParameters SortingParameters { get; set; }
        public FTDailyDexRateOptionalParameters OptionalParameters { get; set; }

        public FTDailyDexRateRequestParameters()
        {
            FilterParameters = new FTDexRateFilterParameters();
            SortingParameters = new FTDailyRateSortingParameters();
            OptionalParameters = new FTDailyDexRateOptionalParameters();
        }
    }
}
