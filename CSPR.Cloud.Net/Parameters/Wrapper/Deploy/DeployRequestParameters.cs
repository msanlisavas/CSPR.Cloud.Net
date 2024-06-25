using CSPR.Cloud.Net.Parameters.OptionalParameters.Deploy;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Deploy
{
    /// <summary>
    /// Represents request parameters for retrieving deploys with optional parameters.
    /// </summary>
    public class DeployRequestParameters
    {
        /// <summary>
        /// Gets or sets the optional parameters for the request.
        /// OptionalParameters are used to include them in the results according to its parameters.
        /// </summary>
        public DeployOptionalParameters OptionalParameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeployRequestParameters"/> class.
        /// </summary>
        public DeployRequestParameters()
        {
            OptionalParameters = new DeployOptionalParameters();
        }
    }

}
