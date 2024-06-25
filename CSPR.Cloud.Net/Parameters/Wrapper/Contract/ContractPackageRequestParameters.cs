using CSPR.Cloud.Net.Parameters.Filtering.Contract;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Contract;
using CSPR.Cloud.Net.Parameters.Sorting.Contract;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Contract
{
    /// <summary>
    /// Represents request parameters for retrieving contract packages with pagination, filtering, sorting, and optional parameters.
    /// </summary>
    public class ContractPackageRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public ContractPackageFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public ContractPackageSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public ContractPackageOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractPackageRequestParameters"/> class.
        /// </summary>
        public ContractPackageRequestParameters()
        {
            FilterParameters = new ContractPackageFilterParameters();
            SortingParameters = new ContractPackageSortingParameters();
            OptionalParameters = new ContractPackageOptionalParameters();
        }
    }

}
