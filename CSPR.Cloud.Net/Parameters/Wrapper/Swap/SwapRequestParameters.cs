using CSPR.Cloud.Net.Parameters.Filtering.Swap;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Swap;
using CSPR.Cloud.Net.Parameters.Sorting.Swap;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Swap
{
    public class SwapRequestParameters : Paginated
    {
        public SwapFilterParameters FilterParameters { get; set; }
        public SwapSortingParameters SortingParameters { get; set; }
        public SwapOptionalParameters OptionalParameters { get; set; }

        public SwapRequestParameters()
        {
            FilterParameters = new SwapFilterParameters();
            SortingParameters = new SwapSortingParameters();
            OptionalParameters = new SwapOptionalParameters();
        }
    }
}
