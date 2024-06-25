using CSPR.Cloud.Net.Parameters.Filtering.Account;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.Sorting.Account;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Accounts
{
    /// <summary>
    /// Represents request parameters for retrieving account information with pagination, filtering, sorting, and optional parameters.
    /// </summary>
    public class AccountInfosRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public AccountInfosFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public AccountInfosSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountInfosRequestParameters"/> class.
        /// </summary>
        public AccountInfosRequestParameters()
        {
            FilterParameters = new AccountInfosFilterParameters();
            SortingParameters = new AccountInfosSortingParameters();
        }
    }


}
