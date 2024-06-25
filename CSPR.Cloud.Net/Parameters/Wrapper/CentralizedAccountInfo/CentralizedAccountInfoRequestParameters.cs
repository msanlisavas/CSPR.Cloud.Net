using CSPR.Cloud.Net.Parameters.Filtering.CentralizedAccountInfo;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.Sorting.CentralizedAccountInfo;

namespace CSPR.Cloud.Net.Parameters.Wrapper.CentralizedAccountInfo
{
    /// <summary>
    /// Represents request parameters for retrieving centralized account information with pagination, filtering, and sorting options.
    /// </summary>
    public class CentralizedAccountInfoRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public CentralizedAccountInfoFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public CentralizedAccountInfoSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CentralizedAccountInfoRequestParameters"/> class.
        /// </summary>
        public CentralizedAccountInfoRequestParameters()
        {
            FilterParameters = new CentralizedAccountInfoFilterParameters();
            SortingParameters = new CentralizedAccountInfoSortingParameters();
        }
    }

}
