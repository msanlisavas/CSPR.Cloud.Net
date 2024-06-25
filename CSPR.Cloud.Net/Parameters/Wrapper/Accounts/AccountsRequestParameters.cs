using CSPR.Cloud.Net.Parameters.Filtering.Account;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Account;
using CSPR.Cloud.Net.Parameters.Sorting.Account;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Accounts
{
    /// <summary>
    /// Represents request parameters for retrieving accounts with pagination, filtering, sorting, and optional parameters.
    /// </summary>
    public class AccountsRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public AccountsSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public AccountsOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public AccountsFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsRequestParameters"/> class.
        /// </summary>
        public AccountsRequestParameters()
        {
            SortingParameters = new AccountsSortingParameters();
            OptionalParameters = new AccountsOptionalParameters();
            FilterParameters = new AccountsFilterParameters()
            {
                AccountHashes = new List<string>()
            };
        }
    }

}
