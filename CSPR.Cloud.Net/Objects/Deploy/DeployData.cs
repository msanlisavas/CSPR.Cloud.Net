using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.Args;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using CSPR.Cloud.Net.Objects.Contract;
using CSPR.Cloud.Net.Objects.Ft;
using CSPR.Cloud.Net.Objects.Nft;
using CSPR.Cloud.Net.Objects.Transfer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Objects.Deploy
{
    /// <summary>
    /// The Deploy entity provides a normalized representation of the Casper Network deploy.
    /// It is enriched with normalized contract data to make it possible to filter deploys by the contract regardless of the execution type.
    /// For more information, see <see href="https://docs.cspr.cloud/rest-api/deploy">CSPR Cloud API documentation</see>.
    /// </summary>
    public class DeployData
    {
        /// <summary>
        /// Deploy hash represented as a hexadecimal string. Primary deploy identifier.
        /// </summary>
        [JsonProperty("deploy_hash")]
        public string DeployHash { get; set; }

        /// <summary>
        /// Hash of the block containing the deploy represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("block_hash")]
        public string BlockHash { get; set; }

        /// <summary>
        /// Height of the block containing the deploy.
        /// </summary>
        [JsonProperty("block_height")]
        public ulong? BlockHeight { get; set; }

        /// <summary>
        /// Public key of the deploy caller account represented as a hexadecimal string.
        /// </summary>
        [JsonProperty("caller_public_key")]
        public string CallerPublicKey { get; set; }

        /// <summary>
        /// Account hash of the deploy caller represented as a hexadecimal string (v2.0.12+).
        /// </summary>
        [JsonProperty("caller_hash")]
        public string CallerHash { get; set; }

        /// <summary>
        /// The deploy caller's registered CSPR.name when the <c>caller_cspr_name</c> includer is requested (v2.1.0+).
        /// </summary>
        [JsonProperty("caller_cspr_name")]
        public string CallerCsprName { get; set; }

        /// <summary>
        /// DeployExecutionType unique identifier.
        /// </summary>
        [JsonProperty("execution_type_id")]
        public byte? ExecutionTypeId { get; set; }

        /// <summary>
        /// Casper 2.0 transaction version identifier (1 for deploys, 2 for transactions).
        /// </summary>
        [JsonProperty("version_id")]
        public byte? VersionId { get; set; }

        /// <summary>
        /// Casper 2.0 pricing mode identifier.
        /// </summary>
        [JsonProperty("pricing_mode_id")]
        public byte? PricingModeId { get; set; }

        /// <summary>
        /// Casper 2.0 maximum gas price the caller is willing to pay.
        /// </summary>
        [JsonProperty("gas_price_limit")]
        public ulong? GasPriceLimit { get; set; }

        /// <summary>
        /// Whether the transaction uses the standard payment logic (Casper 2.0).
        /// </summary>
        [JsonProperty("is_standard_payment")]
        public bool? IsStandardPayment { get; set; }

        /// <summary>
        /// Casper 2.0 runtime type identifier (e.g. VM Casper v1 / WASM v2).
        /// </summary>
        [JsonProperty("runtime_type_id")]
        public byte? RuntimeTypeId { get; set; }

        /// <summary>
        /// Actual gas consumed by the transaction, in motes. Typed as string to avoid uint64 overflow.
        /// </summary>
        [JsonProperty("consumed_gas")]
        public string ConsumedGas { get; set; }

        /// <summary>
        /// Refund amount returned to the caller in motes (v2.0.12+). Typed as string to avoid uint64 overflow.
        /// </summary>
        [JsonProperty("refund_amount")]
        public string RefundAmount { get; set; }

        /// <summary>
        /// Hash of the contract package called by the deploy represented as a hexadecimal string. null if the deploy had no contract call.
        /// </summary>
        [JsonProperty("contract_package_hash")]
        public string ContractPackageHash { get; set; }

        /// <summary>
        /// Hash of the contract called by the deploy represented as a hexadecimal string. null if the deploy had no contract call.
        /// </summary>
        [JsonProperty("contract_hash")]
        public string ContractHash { get; set; }

        /// <summary>
        /// Identifier of the ContractEntrypoint called by deploy. null if the deploy had no contract call.
        /// </summary>
        [JsonProperty("entry_point_id")]
        public uint? EntryPointId { get; set; }

        /// <summary>
        /// Deploy sessions arguments provided for contract execution.
        /// </summary>
        [JsonProperty("args")]
        public ArgsData Args { get; set; }

        /// <summary>
        /// Payment amount provided by the caller in motes. The type is string to avoid overflow in languages that don't support uint64, which is the correct type. null if a custom payment contract was provided to the deploy instead of the value in motes.
        /// </summary>
        [JsonProperty("payment_amount")]
        public string PaymentAmount { get; set; }

        /// <summary>
        /// Deploy execution cost. The type is string to avoid overflow in languages that don't support uint64, which is the correct type.
        /// </summary>
        [JsonProperty("cost")]
        public string Cost { get; set; }

        /// <summary>
        /// Error message in case of a failed deploy. null for successful deploys.
        /// </summary>
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Deploy status (pending, expired, or processed).
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Deploy creation timestamp in the ISO 8601 format.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// Additional account information of the deploy caller.
        /// </summary>
        [JsonProperty("account_info")]
        public AccountInfoData AccountInfo { get; set; }

        /// <summary>
        /// Centralized account information of the deploy caller.
        /// </summary>
        [JsonProperty("centralized_account_info")]
        public CentralizedAccountInfoData CentralizedAccountInfo { get; set; }

        /// <summary>
        /// Data about the contract package called by the deploy.
        /// </summary>
        [JsonProperty("contract_package")]
        public ContractPackageData ContractPackage { get; set; }

        /// <summary>
        /// Data about the contract called by the deploy.
        /// </summary>
        [JsonProperty("contract")]
        public ContractData Contract { get; set; }

        /// <summary>
        /// Data about the contract entry point called by the deploy.
        /// </summary>
        [JsonProperty("contract_entrypoint")]
        public EntryPointData ContractEntrypoint { get; set; }

        /// <summary>
        /// Rate of the deploy.
        /// </summary>
        [JsonProperty("rate")]
        public float? Rate { get; set; }

        /// <summary>
        /// List of transfer data associated with the deploy.
        /// </summary>
        [JsonProperty("transfers")]
        public List<TransferData> Transfers { get; set; }

        /// <summary>
        /// List of NFT token actions associated with the deploy.
        /// </summary>
        [JsonProperty("nft_token_actions")]
        public List<NFTTokenActionData> NFTTokenAction { get; set; }

        /// <summary>
        /// List of FT token actions associated with the deploy.
        /// </summary>
        [JsonProperty("ft_token_actions")]
        public List<FTTokenActionData> FTTokenAction { get; set; }
    }
}
