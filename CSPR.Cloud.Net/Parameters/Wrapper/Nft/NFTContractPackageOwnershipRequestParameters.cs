using CSPR.Cloud.Net.Parameters.Filtering.Nft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Nft;
using CSPR.Cloud.Net.Parameters.Sorting.Nft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Nft
{
    public class NFTContractPackageOwnershipRequestParameters : Paginated
    {
        public NFTContractPackageOwnershipOptionalParameters OptionalParameters { get; set; }
        public NFTContractPackageOwnershipFilterParameters FilterParameters { get; set; }
        public NFTContractPackageOwnershipSortingParameters SortingParameters { get; set; }
        public NFTContractPackageOwnershipRequestParameters()
        {
            OptionalParameters = new NFTContractPackageOwnershipOptionalParameters();
            FilterParameters = new NFTContractPackageOwnershipFilterParameters();
            SortingParameters = new NFTContractPackageOwnershipSortingParameters();
        }
    }
}
