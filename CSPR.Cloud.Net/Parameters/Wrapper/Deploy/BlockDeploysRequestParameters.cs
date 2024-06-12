using CSPR.Cloud.Net.Parameters.Filtering.Deploy;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Deploy;
using CSPR.Cloud.Net.Parameters.Sorting.Deploy;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Deploy
{
    public class BlockDeploysRequestParameters : Paginated
    {
        public DeployOptionalParameters OptionalParameters { get; set; }
        public BlockDeploysFilterParameters FilterParameters { get; set; }
        public DeploysSortingParameters SortingParameters { get; set; }
        public BlockDeploysRequestParameters()
        {
            OptionalParameters = new DeployOptionalParameters();
            FilterParameters = new BlockDeploysFilterParameters();
            SortingParameters = new DeploysSortingParameters();
        }
    }
}
