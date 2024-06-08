using CSPR.Cloud.Net.Parameters.Filtering.Contract;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Contract;
using CSPR.Cloud.Net.Parameters.Sorting.Contract;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Contract
{
    public class ContractPackageRequestParameters : Paginated
    {
        public ContractPackageFilterParameters FilterParameters { get; set; }
        public ContractPackageSortingParameters SortingParameters { get; set; }
        public ContractPackageOptionalParameters OptionalParameters { get; set; }
        public ContractPackageRequestParameters()
        {
            FilterParameters = new ContractPackageFilterParameters();
            SortingParameters = new ContractPackageSortingParameters();
            OptionalParameters = new ContractPackageOptionalParameters();
        }
    }
}
