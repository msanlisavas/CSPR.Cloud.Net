using CSPR.Cloud.Net.Parameters.Filtering.Validator;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Validator;
using CSPR.Cloud.Net.Parameters.Sorting.Validator;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Validator
{
    /// <summary>
    /// Represents request parameters for retrieving validator rewards with pagination, filtering,
    /// sorting, and optional parameters.
    /// </summary>
    public class ValidatorRewardsRequestParameters : Paginated
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// </summary>
        public ValidatorRewardsOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the filter parameters for the request. Supports era-range filtering (v2.4.0+).
        /// </summary>
        public ValidatorRewardsFilterParameters FilterParameters { get; set; }

        /// <summary>
        /// Gets or sets the sorting parameters for the request.
        /// </summary>
        public ValidatorRewardsSortingParameters SortingParameters { get; set; }

        public ValidatorRewardsRequestParameters()
        {
            OptionalParameters = new ValidatorRewardsOptionalParameters();
            FilterParameters = new ValidatorRewardsFilterParameters();
            SortingParameters = new ValidatorRewardsSortingParameters();
        }
    }

}
