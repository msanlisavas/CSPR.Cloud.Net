using CSPR.Cloud.Net.Parameters.Filtering.Ft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Ft;
using CSPR.Cloud.Net.Parameters.Sorting.Ft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Ft
{
    public class FTAccountActionRequestParameters : Paginated
    {
        public FTActionOptionalParameters OptionalParameters { get; set; }
        public FTAccountActionFilterParameters FilterParameters { get; set; }
        public FTAccountActionSortingParameters SortingParameters { get; set; }
        public FTAccountActionRequestParameters()
        {
            OptionalParameters = new FTActionOptionalParameters();
            FilterParameters = new FTAccountActionFilterParameters();
            SortingParameters = new FTAccountActionSortingParameters();
        }
    }
}
