using CSPR.Cloud.Net.Parameters.Filtering.Nft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Nft;
using CSPR.Cloud.Net.Parameters.Sorting.Nft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Nft
{
    /// <summary>
    /// Represents request parameters for retrieving NFT contract package ownership with pagination, filtering, sorting, and optional parameters.
    /// </summary>
    public class NFTContractPackageOwnershipRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public NFTContractPackageOwnershipOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public NFTContractPackageOwnershipFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public NFTContractPackageOwnershipSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NFTContractPackageOwnershipRequestParameters"/> class.
        /// </summary>
        public NFTContractPackageOwnershipRequestParameters()
        {
            OptionalParameters = new NFTContractPackageOwnershipOptionalParameters();
            FilterParameters = new NFTContractPackageOwnershipFilterParameters();
            SortingParameters = new NFTContractPackageOwnershipSortingParameters();
        }
    }

}
