using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Delegate;
using CSPR.Cloud.Net.Parameters.Sorting.Delegate;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Delegate
{
    /// <summary>
    /// Represents request parameters for retrieving delegations with pagination, sorting, and optional parameters.
    /// </summary>
    public class DelegationRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public DelegationOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public DelegationSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegationRequestParameters"/> class.
        /// </summary>
        public DelegationRequestParameters()
        {
            OptionalParameters = new DelegationOptionalParameters();
            SortingParameters = new DelegationSortingParameters();
        }
    }

}
