using CSPR.Cloud.Net.Parameters.Filtering.Nft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Nft;
using CSPR.Cloud.Net.Parameters.Sorting.Nft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Nft
{
    public class NFTContractPackageActionsRequestParameters : Paginated
    {
        public NFTContractPackageActionsOptionalParameters OptionalParameters { get; set; }
        public NFTContractPackageActionsSortingParameters SortingParameters { get; set; }
        public NFTContractPackageActionsFilterParameters FilterParameters { get; set; }
        public NFTContractPackageActionsRequestParameters()
        {
            OptionalParameters = new NFTContractPackageActionsOptionalParameters();
            SortingParameters = new NFTContractPackageActionsSortingParameters();
            FilterParameters = new NFTContractPackageActionsFilterParameters();
        }
    }
}
