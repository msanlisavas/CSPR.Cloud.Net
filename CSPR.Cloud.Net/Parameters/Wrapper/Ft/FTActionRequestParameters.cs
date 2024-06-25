using CSPR.Cloud.Net.Parameters.Filtering.Ft;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Ft;
using CSPR.Cloud.Net.Parameters.Sorting.Ft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Ft
{
    /// <summary>
    /// Represents request parameters for retrieving FT actions with pagination, filtering, sorting, and optional parameters.
    /// </summary>
    public class FTActionRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public FTActionOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public FTActionFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public FTActionSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FTActionRequestParameters"/> class.
        /// </summary>
        public FTActionRequestParameters()
        {
            OptionalParameters = new FTActionOptionalParameters();
            FilterParameters = new FTActionFilterParameters();
            SortingParameters = new FTActionSortingParameters();
        }
    }

}
