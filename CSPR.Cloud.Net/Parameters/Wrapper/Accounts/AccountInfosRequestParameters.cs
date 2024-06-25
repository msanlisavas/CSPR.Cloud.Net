using CSPR.Cloud.Net.Parameters.Filtering.Account;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.Sorting.Account;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Accounts
{
    public class AccountInfosRequestParameters : Paginated
    {
        public AccountInfosFilterParameters FilterParameters { get; set; }
        public AccountInfosSortingParameters SortingParameters { get; set; }
        public AccountInfosRequestParameters()
        {
            FilterParameters = new AccountInfosFilterParameters();
            SortingParameters = new AccountInfosSortingParameters();

        }
    }
}
