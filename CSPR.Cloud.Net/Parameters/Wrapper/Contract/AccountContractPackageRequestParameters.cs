using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Contract;
using CSPR.Cloud.Net.Parameters.Sorting.Contract;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Contract
{
    public class AccountContractPackageRequestParameters : Paginated
    {
        public ContractPackageSortingParameters SortingParameters { get; set; }
        public ContractPackageOptionalParameters OptionalParameters { get; set; }
        public AccountContractPackageRequestParameters()
        {
            SortingParameters = new ContractPackageSortingParameters();
            OptionalParameters = new ContractPackageOptionalParameters();
        }
    }
}
