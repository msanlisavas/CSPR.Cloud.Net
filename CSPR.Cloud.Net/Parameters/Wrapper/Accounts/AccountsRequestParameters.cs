using CSPR.Cloud.Net.Parameters.OptionalParameters.Account;
using CSPR.Cloud.Net.Parameters.Sorting.Account;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Accounts
{
    public class AccountsRequestParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public AccountsSorting Sorting { get; set; }
        public AccountsOptionalParameters OptionalParameters { get; set; }
        public AccountsQueryParameters QueryParameters { get; set; }

        public AccountsRequestParameters()
        {
            Sorting = new AccountsSorting();
            OptionalParameters = new AccountsOptionalParameters();
            QueryParameters = new AccountsQueryParameters()
            {
                AccountHashes = new List<string>()
            };
        }
    }
}
