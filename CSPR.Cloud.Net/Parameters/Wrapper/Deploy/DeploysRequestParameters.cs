using CSPR.Cloud.Net.Parameters.Filtering.Deploy;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Deploy;
using CSPR.Cloud.Net.Parameters.Sorting.Deploy;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Deploy
{
    public class DeploysRequestParameters : Paginated
    {
        public DeployOptionalParameters OptionalParameters { get; set; }
        public DeploysFilterParameters FilterParameters { get; set; }
        public DeploysSortingParameters SortingParameters { get; set; }
        public DeploysRequestParameters()
        {
            OptionalParameters = new DeployOptionalParameters();
            FilterParameters = new DeploysFilterParameters();
            SortingParameters = new DeploysSortingParameters();
        }
    }
}
