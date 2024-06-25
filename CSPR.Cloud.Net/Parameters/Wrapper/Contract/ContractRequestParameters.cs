using CSPR.Cloud.Net.Parameters.OptionalParameters.Contract;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Contract
{
    /// <summary>
    /// Represents request parameters for retrieving contracts with optional parameters.
    /// </summary>
    public class ContractRequestParameters
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public ContractOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractRequestParameters"/> class.
        /// </summary>
        public ContractRequestParameters()
        {
            OptionalParameters = new ContractOptionalParameters();
        }
    }

}
