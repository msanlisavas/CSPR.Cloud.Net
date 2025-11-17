using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Ft;
using CSPR.Cloud.Net.Parameters.Sorting.Ft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Ft
{
    /// <summary>
    /// Represents request parameters for retrieving contract package FT ownership with pagination, sorting, and optional parameters.
    /// </summary>
    public class FTContractPackageOwnershipRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to order the results according to its parameters.
        /// </summary>
        public FTOwnershipSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public FTContractPackageOwnershipOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FTContractPackageOwnershipRequestParameters"/> class.
        /// </summary>
        public FTContractPackageOwnershipRequestParameters()
        {
            SortingParameters = new FTOwnershipSortingParameters();
            OptionalParameters = new FTContractPackageOwnershipOptionalParameters();
        }
    }

}
