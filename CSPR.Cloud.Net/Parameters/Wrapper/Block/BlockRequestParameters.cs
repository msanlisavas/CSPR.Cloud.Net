using CSPR.Cloud.Net.Parameters.Filtering.Block;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Block;
using CSPR.Cloud.Net.Parameters.Sorting.Block;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Block
{
    public class BlockRequestParameters : Paginated
    {
        public BlockSortingParameters Sorting { get; set; }
        public BlockOptionalParameters OptionalParameters { get; set; }
        public BlockFilterParameters FilterParameters { get; set; }

        public BlockRequestParameters()
        {
            Sorting = new BlockSortingParameters();
            OptionalParameters = new BlockOptionalParameters();
            FilterParameters = new BlockFilterParameters();
        }
    }
}
