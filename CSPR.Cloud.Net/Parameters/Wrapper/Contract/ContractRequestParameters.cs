using CSPR.Cloud.Net.Parameters.OptionalParameters.Contract;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Contract
{
    public class ContractRequestParameters
    {
        public ContractOptionalParameters OptionalParameters { get; set; }
        public ContractRequestParameters()
        {
            OptionalParameters = new ContractOptionalParameters();
        }
    }
}
