using CSPR.Cloud.Net.Parameters.Filtering.Deploy;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Deploy;
using CSPR.Cloud.Net.Parameters.Sorting.Deploy;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Deploy
{
    /// <summary>
    /// Represents request parameters for retrieving deploys with pagination, filtering, sorting, and optional parameters.
    /// </summary>
    public class DeploysRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public DeployOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public DeploysFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public DeploysSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeploysRequestParameters"/> class.
        /// </summary>
        public DeploysRequestParameters()
        {
            OptionalParameters = new DeployOptionalParameters();
            FilterParameters = new DeploysFilterParameters();
            SortingParameters = new DeploysSortingParameters();
        }
    }

}
