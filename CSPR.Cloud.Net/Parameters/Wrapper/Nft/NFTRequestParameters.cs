using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Nft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Nft
{
    /// <summary>
    /// Represents request parameters for retrieving NFTs with pagination and optional parameters.
    /// </summary>
    public class NFTRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public NFTOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NFTRequestParameters"/> class.
        /// </summary>
        public NFTRequestParameters()
        {
            OptionalParameters = new NFTOptionalParameters();
        }
    }

}
