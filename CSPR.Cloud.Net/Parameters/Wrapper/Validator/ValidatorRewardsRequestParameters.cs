using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Validator;
using CSPR.Cloud.Net.Parameters.Sorting.Validator;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Validator
{
    public class ValidatorRewardsRequestParameters : Paginated
    {
        public ValidatorRewardsOptionalParameters OptionalParameters { get; set; }
        public ValidatorRewardsSortingParameters SortingParameters { get; set; }
        public ValidatorRewardsRequestParameters()
        {
            OptionalParameters = new ValidatorRewardsOptionalParameters();
            SortingParameters = new ValidatorRewardsSortingParameters();

        }
    }
}
