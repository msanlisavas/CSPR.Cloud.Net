using CSPR.Cloud.Net.Parameters.Filtering.Transfer;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Transfer;
using CSPR.Cloud.Net.Parameters.Sorting.Transfer;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Transfer
{
    public class TransferAccountRequestParameters : Paginated
    {
        public TransferAccountOptionalParameters OptionalParameters { get; set; }
        public TransferAccountSortingParameters SortingParameters { get; set; }
        public TransferAccountFilterParameters FilterParameters { get; set; }
        public TransferAccountRequestParameters()
        {
            OptionalParameters = new TransferAccountOptionalParameters();
            SortingParameters = new TransferAccountSortingParameters();
            FilterParameters = new TransferAccountFilterParameters();

        }
    }
}
