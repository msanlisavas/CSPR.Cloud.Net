using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Contract
{
    /// <summary>
    /// Represents optional parameters
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/optional-properties">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class ContractPackageOptionalParameters
    {
        /// <summary>
        /// Gets or sets the number of deploys in the specified number of the past days.
        /// This property accepts the number of days as an argument.
        /// </summary>
        [JsonProperty("deploys_number")]
        public int? DeploysNumber { get; set; } = 0;


    }
}
