using CSPR.Cloud.Net.Parameters.Filtering.Nft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Nft;
using CSPR.Cloud.Net.Parameters.Sorting.Nft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Nft
{
    public class NFTContractPackageRequestParameters : Paginated
    {
        public NFTContractPackageOptionalParameters OptionalParameters { get; set; }
        public NFTContractPackageFilterParameters FilterParameters { get; set; }
        public NFTContractPackageSortingParameters SortingParameters { get; set; }
        public NFTContractPackageRequestParameters()
        {
            OptionalParameters = new NFTContractPackageOptionalParameters();
            FilterParameters = new NFTContractPackageFilterParameters();
            SortingParameters = new NFTContractPackageSortingParameters();
        }
    }
}
