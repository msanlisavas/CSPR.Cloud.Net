using CSPR.Cloud.Net.Parameters.Filtering.Nft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Nft;
using CSPR.Cloud.Net.Parameters.Sorting.Nft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Nft
{
    /// <summary>
    /// Request parameters for the unscoped NFT listing endpoint (<c>/nft-tokens</c>).
    /// </summary>
    public class NFTsRequestParameters : Paginated
    {
        /// <summary>
        /// Optional inclusion flags (e.g., <c>contract_package</c>, <c>owner_public_key</c>).
        /// </summary>
        public NFTOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Filters narrowing the listing by contract package, owner, or block-height range.
        /// </summary>
        public NFTsFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Sorting parameters. Supports sorting by timestamp.
        /// </summary>
        public NFTsSortingParameters SortingParameters { get; set; }

        public NFTsRequestParameters()
        {
            OptionalParameters = new NFTOptionalParameters();
            FilterParameters = new NFTsFilterParameters();
            SortingParameters = new NFTsSortingParameters();
        }
    }
}
