using CSPR.Cloud.Net.Parameters.Filtering.Validator;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Validator;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Validator
{
    public class ValidatorRequestParameters
    {
        public ValidatorFilterParameters FilterParameters { get; set; }
        public ValidatorOptionalParameters OptionalParameters { get; set; }
        public ValidatorRequestParameters()
        {
            FilterParameters = new ValidatorFilterParameters();
            OptionalParameters = new ValidatorOptionalParameters();

        }
    }
}
