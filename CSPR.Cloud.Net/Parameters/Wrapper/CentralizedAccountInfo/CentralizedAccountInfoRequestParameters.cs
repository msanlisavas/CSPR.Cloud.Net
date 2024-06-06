using CSPR.Cloud.Net.Parameters.Filtering.CentralizedAccountInfo;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.Sorting.CentralizedAccountInfo;

namespace CSPR.Cloud.Net.Parameters.Wrapper.CentralizedAccountInfo
{
    public class CentralizedAccountInfoRequestParameters : Paginated
    {
        public CentralizedAccountInfoFilterParameters FilterParameters { get; set; }
        public CentralizedAccountInfoSortingParameters SortingParameters { get; set; }
        public CentralizedAccountInfoRequestParameters()
        {
            FilterParameters = new CentralizedAccountInfoFilterParameters();
            SortingParameters = new CentralizedAccountInfoSortingParameters();
        }
    }
}
