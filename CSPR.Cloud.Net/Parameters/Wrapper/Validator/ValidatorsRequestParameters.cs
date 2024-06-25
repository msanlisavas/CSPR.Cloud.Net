using CSPR.Cloud.Net.Parameters.Filtering.Validator;
using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Validator;
using CSPR.Cloud.Net.Parameters.Sorting.Validator;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Validator
{
    public class ValidatorsRequestParameters : Paginated
    {
        public ValidatorsFilterParameters FilterParameters { get; set; }
        public ValidatorsOptionalParameters OptionalParameters { get; set; }
        public ValidatorsSortingParameters SortingParameters { get; set; }
        public ValidatorsRequestParameters()
        {
            FilterParameters = new ValidatorsFilterParameters();
            OptionalParameters = new ValidatorsOptionalParameters();
            SortingParameters = new ValidatorsSortingParameters();

        }
    }
}
