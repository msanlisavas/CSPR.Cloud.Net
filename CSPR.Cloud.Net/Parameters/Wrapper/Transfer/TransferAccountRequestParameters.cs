using CSPR.Cloud.Net.Parameters.Filtering.Transfer;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Transfer;
using CSPR.Cloud.Net.Parameters.Sorting.Transfer;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Transfer
{
    /// <summary>
    /// Represents request parameters for retrieving transfer accounts with pagination, filtering, sorting, and optional parameters.
    /// </summary>
    public class TransferAccountRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public TransferAccountOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public TransferAccountSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public TransferAccountFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransferAccountRequestParameters"/> class.
        /// </summary>
        public TransferAccountRequestParameters()
        {
            OptionalParameters = new TransferAccountOptionalParameters();
            SortingParameters = new TransferAccountSortingParameters();
            FilterParameters = new TransferAccountFilterParameters();
        }
    }

}
