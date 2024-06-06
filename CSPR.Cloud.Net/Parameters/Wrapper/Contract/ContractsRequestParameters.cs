using CSPR.Cloud.Net.Parameters.Filtering.Contract;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Contract;
using CSPR.Cloud.Net.Parameters.Sorting.Contract;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Contract
{
    public class ContractsRequestParameters : Paginated
    {
        public ContractsFilterParameters FilterParameters { get; set; }
        public ContractOptionalParameters OptionalParameters { get; set; }
        public ContractsSortingParameters SortingParameters { get; set; }
        public ContractsRequestParameters()
        {
            OptionalParameters = new ContractOptionalParameters();
            FilterParameters = new ContractsFilterParameters();
            SortingParameters = new ContractsSortingParameters();
        }
    }
}
