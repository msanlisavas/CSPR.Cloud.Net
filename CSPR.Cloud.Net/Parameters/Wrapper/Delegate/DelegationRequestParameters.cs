using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Delegate;
using CSPR.Cloud.Net.Parameters.Sorting.Delegate;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Delegate
{
    public class DelegationRequestParameters : Paginated
    {
        public DelegationOptionalParameters OptionalParameters { get; set; }
        public DelegationSortingParameters SortingParameters { get; set; }
        public DelegationRequestParameters()
        {
            OptionalParameters = new DelegationOptionalParameters();
            SortingParameters = new DelegationSortingParameters();
        }
    }
}
