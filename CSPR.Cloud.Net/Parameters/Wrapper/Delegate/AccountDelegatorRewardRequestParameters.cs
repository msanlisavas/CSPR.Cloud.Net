using CSPR.Cloud.Net.Parameters.Filtering.Delegate;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Delegate;
using CSPR.Cloud.Net.Parameters.Sorting.Delegate;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Delegate
{
    /// <summary>
    /// Represents request parameters for retrieving account delegator rewards with pagination, filtering, sorting, and optional parameters.
    /// </summary>
    public class AccountDelegatorRewardRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public AccountDelegatorRewardFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public AccountDelegatorRewardSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public AccountDelegatorRewardOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountDelegatorRewardRequestParameters"/> class.
        /// </summary>
        public AccountDelegatorRewardRequestParameters()
        {
            FilterParameters = new AccountDelegatorRewardFilterParameters();
            SortingParameters = new AccountDelegatorRewardSortingParameters();
            OptionalParameters = new AccountDelegatorRewardOptionalParameters();
        }
    }

}
