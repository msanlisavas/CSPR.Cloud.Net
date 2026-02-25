using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Validator;
using CSPR.Cloud.Net.Parameters.Sorting.Validator;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Validator
{
    public class ValidatorEraRewardsRequestParameters : Paginated
    {
        public ValidatorRewardsSortingParameters SortingParameters { get; set; }
        public ValidatorRewardsOptionalParameters OptionalParameters { get; set; }

        public ValidatorEraRewardsRequestParameters()
        {
            SortingParameters = new ValidatorRewardsSortingParameters();
            OptionalParameters = new ValidatorRewardsOptionalParameters();
        }
    }
}
