using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Objects.Delegate
{
    /// <summary>
    /// The Delegation entity represents a record of a delegation transaction in the context of Casper Network Staking vs. Delegating process.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/delegation">CSPR Cloud API documentation</see>.
    /// </summary>
    public class DelegationData
    {
        /// <summary>
        /// Public key of the delegator represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        /// <summary>
        /// Public key of the validator represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("validator_public_key")]
        public string ValidatorPublicKey { get; set; }

        /// <summary>
        /// Delegation amount in motes. The type is string to avoid overflow in languages that don't support uint64, which is the correct type.
        /// </summary>
        [JsonProperty("stake")]
        public string Stake { get; set; }

        /// <summary>
        /// URef of the purse from which the delegation was made in the uref-dead...beef-007 format.
        /// </summary>
        [JsonProperty("bonding_purse")]
        public string BondingPurse { get; set; }

        /// <summary>
        /// Additional account information of the delegator.
        /// </summary>
        [JsonProperty("account_info")]
        public AccountInfoData AccountInfo { get; set; }

        /// <summary>
        /// Additional account information of the validator.
        /// </summary>
        [JsonProperty("validator_account_info")]
        public AccountInfoData ValidatorAccountInfo { get; set; }

        /// <summary>
        /// Centralized account information.
        /// </summary>
        [JsonProperty("centralized_account_info")]
        public CentralizedAccountInfoData CentralizedAccountInfo { get; set; }
    }

}
