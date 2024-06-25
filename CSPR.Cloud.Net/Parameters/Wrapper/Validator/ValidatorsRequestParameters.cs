using CSPR.Cloud.Net.Parameters.Filtering.Validator;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Validator;
using CSPR.Cloud.Net.Parameters.Sorting.Validator;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Validator
{
    /// <summary>
    /// Represents request parameters for retrieving validators with pagination, filtering, sorting, and optional parameters.
    /// </summary>
    public class ValidatorsRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public ValidatorsFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public ValidatorsOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public ValidatorsSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorsRequestParameters"/> class.
        /// </summary>
        public ValidatorsRequestParameters()
        {
            FilterParameters = new ValidatorsFilterParameters();
            OptionalParameters = new ValidatorsOptionalParameters();
            SortingParameters = new ValidatorsSortingParameters();
        }
    }

}
