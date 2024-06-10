using CSPR.Cloud.Net.Parameters.OptionalParameters.Deploy;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Deploy
{
    public class DeployRequestParameters
    {
        public DeployOptionalParameters OptionalParameters { get; set; }
        public DeployRequestParameters()
        {
            OptionalParameters = new DeployOptionalParameters();
        }
    }
}
