using CSPR.Cloud.Net.Parameters.Filtering.Nft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Nft;
using CSPR.Cloud.Net.Parameters.Sorting.Nft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Nft
{
    /// <summary>
    /// Represents request parameters for retrieving NFT account information with pagination, filtering, sorting, and optional parameters.
    /// </summary>
    public class NFTAccountRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public NFTOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public NFTAccountFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public NFTAccountSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NFTAccountRequestParameters"/> class.
        /// </summary>
        public NFTAccountRequestParameters()
        {
            OptionalParameters = new NFTOptionalParameters();
            FilterParameters = new NFTAccountFilterParameters();
            SortingParameters = new NFTAccountSortingParameters();
        }
    }

}
