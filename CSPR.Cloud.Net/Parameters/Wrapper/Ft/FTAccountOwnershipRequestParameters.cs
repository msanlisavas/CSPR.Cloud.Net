using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Ft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Ft
{
    public class FTAccountOwnershipRequestParameters : Paginated
    {
        public FTAccountOwnershipOptionalParameters OptionalParameters { get; set; }
        public FTAccountOwnershipRequestParameters()
        {
            OptionalParameters = new FTAccountOwnershipOptionalParameters();
        }
    }
}
