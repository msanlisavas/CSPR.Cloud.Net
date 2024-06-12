using CSPR.Cloud.Net.Parameters.Filtering.Ft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Ft;
using CSPR.Cloud.Net.Parameters.Sorting.Ft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Ft
{
    public class FTContractPackageActionRequestParameters : Paginated
    {
        public FTActionOptionalParameters OptionalParameters { get; set; }
        public FTContractPackageActionFilterParameters FilterParameters { get; set; }
        public FTContractPackageActionSortingParameters SortingParameters { get; set; }
        public FTContractPackageActionRequestParameters()
        {
            OptionalParameters = new FTActionOptionalParameters();
            FilterParameters = new FTContractPackageActionFilterParameters();
            SortingParameters = new FTContractPackageActionSortingParameters();
        }
    }
}
