using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Abstract
{
    /// <summary>
    /// Envelope for the single-contract-package responses.
    /// </summary>
    /// <remarks>
    /// The four non-<see cref="Data"/> properties below are never populated. The API nests those
    /// values inside the <c>data</c> object, not beside it, so the envelope-level bindings match no
    /// key in any response. <see cref="Data"/> carries them correctly — on
    /// <c>ContractPackageData</c> they are <c>LatestVersionContractTypeId</c>, <c>Timestamp</c> and
    /// <c>IconUrl</c>. They are kept (deprecated rather than deleted) so existing callers still
    /// compile; they will be removed in the next major version.
    /// </remarks>
    public class ContractResponse<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [System.Obsolete("Never populated: the API nests this inside \"data\". Read Data.LatestVersionContractTypeId instead.")]
        [JsonProperty("latest_version_contract_type_id")]
        public int LatestVersionContractTypeId { get; set; }

        [System.Obsolete("Never populated: the API nests this inside \"data\". Read Data.Timestamp instead.")]
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [System.Obsolete("Never populated: the API nests this inside \"data\". Read Data.IconUrl instead.")]
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        // Non-nullable, so this reads 0 rather than null: a caller cannot tell "no deploys" from
        // "never populated". The envelope does not carry deploys_number at all.
        [System.Obsolete("Never populated, and reads 0 rather than null. Read Data.DeploysNumber, which the contract-package endpoint fills when the deploys_number includer is requested.")]
        [JsonProperty("deploys_number")]
        public int DeploysNumber { get; set; }
    }
}
