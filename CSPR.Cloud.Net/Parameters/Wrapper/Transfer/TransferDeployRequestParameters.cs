using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Transfer;
using CSPR.Cloud.Net.Parameters.Sorting.Transfer;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Transfer
{
    /// <summary>
    /// Represents request parameters for retrieving transfer deploys with pagination, sorting, and optional parameters.
    /// </summary>
    public class TransferDeployRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public TransferDeployOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public TransferDeploySortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransferDeployRequestParameters"/> class.
        /// </summary>
        public TransferDeployRequestParameters()
        {
            OptionalParameters = new TransferDeployOptionalParameters();
            SortingParameters = new TransferDeploySortingParameters();
        }
    }

}
