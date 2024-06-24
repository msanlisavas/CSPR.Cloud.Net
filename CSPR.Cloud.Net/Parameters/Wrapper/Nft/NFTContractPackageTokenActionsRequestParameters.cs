using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Nft;
using CSPR.Cloud.Net.Parameters.Sorting.Nft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Nft
{
    public class NFTContractPackageTokenActionsRequestParameters : Paginated
    {
        public NFTContractPackageTokenActionsOptionalParameters OptionalParameters { get; set; }
        public NFTContractPackageTokenActionsSortingParameters SortingParameters { get; set; }
        public NFTContractPackageTokenActionsRequestParameters()
        {
            OptionalParameters = new NFTContractPackageTokenActionsOptionalParameters();
            SortingParameters = new NFTContractPackageTokenActionsSortingParameters();
        }
    }
}
