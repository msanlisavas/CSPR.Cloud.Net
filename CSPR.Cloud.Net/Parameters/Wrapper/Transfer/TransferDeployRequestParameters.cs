using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Transfer;
using CSPR.Cloud.Net.Parameters.Sorting.Transfer;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Transfer
{
    public class TransferDeployRequestParameters : Paginated
    {
        public TransferDeployOptionalParameters OptionalParameters { get; set; }
        public TransferDeploySortingParameters SortingParameters { get; set; }
        public TransferDeployRequestParameters()
        {
            OptionalParameters = new TransferDeployOptionalParameters();
            SortingParameters = new TransferDeploySortingParameters();

        }
    }
}
