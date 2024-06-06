using CSPR.Cloud.Net.Parameters.Filtering.Contract;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Contract;
using CSPR.Cloud.Net.Parameters.Sorting.Contract;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Contract
{
    public class ByContractRequestParameters : Paginated
    {
        public ByContractPackageFilterParameters FilterParameters { get; set; }
        public ContractOptionalParameters OptionalParameters { get; set; }
        public ContractsSortingParameters SortingParameters { get; set; }
        public ByContractRequestParameters()
        {
            OptionalParameters = new ContractOptionalParameters();
            FilterParameters = new ByContractPackageFilterParameters();
            SortingParameters = new ContractsSortingParameters();
        }
    }
}
