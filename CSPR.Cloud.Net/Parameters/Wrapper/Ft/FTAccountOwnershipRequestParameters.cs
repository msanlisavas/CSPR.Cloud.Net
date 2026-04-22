using CSPR.Cloud.Net.Parameters.Filtering.Ft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Ft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Ft
{
    /// <summary>
    /// Represents request parameters for retrieving FT account ownerships with pagination, filtering,
    /// and optional parameters.
    /// </summary>
    public class FTAccountOwnershipRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// </summary>
        public FTAccountOwnershipOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the filter parameters for the request (v2.6.0+ — <c>contract_package_hash</c>).
        /// </summary>
        public FTAccountOwnershipFilterParameters FilterParameters { get; set; }

        public FTAccountOwnershipRequestParameters()
        {
            OptionalParameters = new FTAccountOwnershipOptionalParameters();
            FilterParameters = new FTAccountOwnershipFilterParameters();
        }
    }

}
