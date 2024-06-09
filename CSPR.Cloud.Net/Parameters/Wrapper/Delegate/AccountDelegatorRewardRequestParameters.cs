using CSPR.Cloud.Net.Parameters.Filtering.Delegate;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Delegate;
using CSPR.Cloud.Net.Parameters.Sorting.Delegate;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Delegate
{
    public class AccountDelegatorRewardRequestParameters : Paginated
    {
        public AccountDelegatorRewardFilterParameters FilterParameters { get; set; }
        public AccountDelegatorRewardSortingParameters SortingParameters { get; set; }
        public AccountDelegatorRewardOptionalParameters OptionalParameters { get; set; }
        public AccountDelegatorRewardRequestParameters()
        {
            FilterParameters = new AccountDelegatorRewardFilterParameters();
            SortingParameters = new AccountDelegatorRewardSortingParameters();
            OptionalParameters = new AccountDelegatorRewardOptionalParameters();
        }
    }
}
