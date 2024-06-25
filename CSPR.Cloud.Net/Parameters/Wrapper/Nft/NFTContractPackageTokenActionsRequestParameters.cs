using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Nft;
using CSPR.Cloud.Net.Parameters.Sorting.Nft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Nft
{
    /// <summary>
    /// Represents request parameters for retrieving NFT contract package token actions with pagination, sorting, and optional parameters.
    /// </summary>
    public class NFTContractPackageTokenActionsRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public NFTContractPackageTokenActionsOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public NFTContractPackageTokenActionsSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NFTContractPackageTokenActionsRequestParameters"/> class.
        /// </summary>
        public NFTContractPackageTokenActionsRequestParameters()
        {
            OptionalParameters = new NFTContractPackageTokenActionsOptionalParameters();
            SortingParameters = new NFTContractPackageTokenActionsSortingParameters();
        }
    }

}
