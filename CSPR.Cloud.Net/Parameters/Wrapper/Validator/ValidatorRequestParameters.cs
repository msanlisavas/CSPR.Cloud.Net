using CSPR.Cloud.Net.Parameters.Filtering.Validator;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Validator;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Validator
{
    /// <summary>
    /// Represents request parameters for retrieving validators with filtering and optional parameters.
    /// </summary>
    public class ValidatorRequestParameters
    {
        /// <summary>
        /// Gets or sets the filter parameters for the request.
        /// FilterParameters are used to filter the results according to its parameters.
        /// </summary>
        public ValidatorFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public ValidatorOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorRequestParameters"/> class.
        /// </summary>
        public ValidatorRequestParameters()
        {
            FilterParameters = new ValidatorFilterParameters();
            OptionalParameters = new ValidatorOptionalParameters();
        }
    }

}
