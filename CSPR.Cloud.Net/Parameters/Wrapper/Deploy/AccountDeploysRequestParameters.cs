using CSPR.Cloud.Net.Parameters.Filtering.Deploy;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Deploy;
using CSPR.Cloud.Net.Parameters.Sorting.Deploy;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Deploy
{
    public class AccountDeploysRequestParameters : Paginated
    {
        public DeployOptionalParameters OptionalParameters { get; set; }
        public AccountDeploysFilterParameters FilterParameters { get; set; }
        public DeploysSortingParameters SortingParameters { get; set; }
        public AccountDeploysRequestParameters()
        {
            OptionalParameters = new DeployOptionalParameters();
            FilterParameters = new AccountDeploysFilterParameters();
            SortingParameters = new DeploysSortingParameters();
        }
    }
}
