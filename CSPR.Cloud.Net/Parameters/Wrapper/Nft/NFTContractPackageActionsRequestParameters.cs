using CSPR.Cloud.Net.Parameters.Filtering.Nft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Nft;
using CSPR.Cloud.Net.Parameters.Sorting.Nft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Nft
{
    /// <summary>
    /// Represents request parameters for retrieving NFT contract package actions with pagination, filtering, sorting, and optional parameters.
    /// </summary>
    public class NFTContractPackageActionsRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public NFTContractPackageActionsOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public NFTContractPackageActionsSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public NFTContractPackageActionsFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NFTContractPackageActionsRequestParameters"/> class.
        /// </summary>
        public NFTContractPackageActionsRequestParameters()
        {
            OptionalParameters = new NFTContractPackageActionsOptionalParameters();
            SortingParameters = new NFTContractPackageActionsSortingParameters();
            FilterParameters = new NFTContractPackageActionsFilterParameters();
        }
    }

}
