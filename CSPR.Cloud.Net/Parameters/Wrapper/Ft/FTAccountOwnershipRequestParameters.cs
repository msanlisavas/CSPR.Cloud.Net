using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Ft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Ft
{
    /// <summary>
    /// Represents request parameters for retrieving FT account ownerships with pagination and optional parameters.
    /// </summary>
    public class FTAccountOwnershipRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public FTAccountOwnershipOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FTAccountOwnershipRequestParameters"/> class.
        /// </summary>
        public FTAccountOwnershipRequestParameters()
        {
            OptionalParameters = new FTAccountOwnershipOptionalParameters();
        }
    }

}
