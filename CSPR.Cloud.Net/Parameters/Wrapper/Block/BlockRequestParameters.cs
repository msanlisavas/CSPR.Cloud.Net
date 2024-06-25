using CSPR.Cloud.Net.Parameters.Filtering.Block;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Block;
using CSPR.Cloud.Net.Parameters.Sorting.Block;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Block
{
    /// <summary>
    /// Represents request parameters for retrieving blocks with pagination, filtering, sorting, and optional parameters.
    /// </summary>
    public class BlockRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public BlockSortingParameters Sorting { get; set; }

        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public BlockOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public BlockFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockRequestParameters"/> class.
        /// </summary>
        public BlockRequestParameters()
        {
            Sorting = new BlockSortingParameters();
            OptionalParameters = new BlockOptionalParameters();
            FilterParameters = new BlockFilterParameters();
        }
    }

}
