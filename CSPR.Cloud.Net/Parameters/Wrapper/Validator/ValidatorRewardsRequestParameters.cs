using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Validator;
using CSPR.Cloud.Net.Parameters.Sorting.Validator;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Validator
{
    /// <summary>
    /// Represents request parameters for retrieving validator rewards with pagination, sorting, and optional parameters.
    /// </summary>
    public class ValidatorRewardsRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public ValidatorRewardsOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// SortingParameters are used to sort the results according to its parameters.
        /// </summary>
        public ValidatorRewardsSortingParameters SortingParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorRewardsRequestParameters"/> class.
        /// </summary>
        public ValidatorRewardsRequestParameters()
        {
            OptionalParameters = new ValidatorRewardsOptionalParameters();
            SortingParameters = new ValidatorRewardsSortingParameters();
        }
    }

}
