using CSPR.Cloud.Net.Parameters.Filtering.Account;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Account;
using CSPR.Cloud.Net.Parameters.Sorting.Account;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Accounts
{
    public class AccountsRequestParameters : Paginated
    {
        public AccountsSortingParameters Sorting { get; set; }
        public AccountsOptionalParameters OptionalParameters { get; set; }
        public AccountsFilterParameters QueryParameters { get; set; }

        public AccountsRequestParameters()
        {
            Sorting = new AccountsSortingParameters();
            OptionalParameters = new AccountsOptionalParameters();
            QueryParameters = new AccountsFilterParameters()
            {
                AccountHashes = new List<string>()
            };
        }
    }
}
