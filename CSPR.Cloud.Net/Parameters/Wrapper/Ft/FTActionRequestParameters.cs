using CSPR.Cloud.Net.Parameters.Filtering.Ft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Ft;
using CSPR.Cloud.Net.Parameters.Sorting.Ft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Ft
{
    public class FTActionRequestParameters : Paginated
    {
        public FTActionOptionalParameters OptionalParameters { get; set; }
        public FTActionFilterParameters FilterParameters { get; set; }
        public FTActionSortingParameters SortingParameters { get; set; }
        public FTActionRequestParameters()
        {
            OptionalParameters = new FTActionOptionalParameters();
            FilterParameters = new FTActionFilterParameters();
            SortingParameters = new FTActionSortingParameters();
        }
    }
}
