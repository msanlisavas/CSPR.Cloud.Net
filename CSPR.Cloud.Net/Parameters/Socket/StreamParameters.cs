using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Socket
{
    /// <summary>
    /// Base class for streaming subscription filters. Fields correspond 1:1 to the query-string
    /// parameters documented at <see href="https://docs.cspr.cloud/streaming-api/reference"/>.
    /// </summary>
    public abstract class StreamParametersBase
    {
    }

    /// <summary>
    /// Filters for the account-balances stream.
    /// </summary>
    public class AccountBalanceStreamParameters : StreamParametersBase
    {
        /// <summary>Account hashes to filter on (comma-joined by the client).</summary>
        public List<string> AccountHash { get; set; }

        /// <summary>Public keys to filter on (comma-joined by the client).</summary>
        public List<string> PublicKey { get; set; }
    }

    /// <summary>
    /// Filters for the blocks stream.
    /// </summary>
    public class BlockStreamParameters : StreamParametersBase
    {
        /// <summary>Proposer public keys to filter on (comma-joined by the client).</summary>
        public List<string> ProposerPublicKey { get; set; }
    }

    /// <summary>
    /// Filters for the contracts stream.
    /// </summary>
    public class ContractStreamParameters : StreamParametersBase
    {
        /// <summary>Contract package hashes to filter on (comma-joined by the client).</summary>
        public List<string> ContractPackageHash { get; set; }

        /// <summary>Deploy hashes to filter on (comma-joined by the client).</summary>
        public List<string> DeployHash { get; set; }
    }

    /// <summary>
    /// Filters for the contract-packages stream.
    /// </summary>
    public class ContractPackageStreamParameters : StreamParametersBase
    {
        /// <summary>Contract package hashes to filter on (comma-joined by the client).</summary>
        public List<string> ContractPackageHash { get; set; }

        /// <summary>Owner public keys to filter on (comma-joined by the client).</summary>
        public List<string> OwnerPublicKey { get; set; }
    }

    /// <summary>
    /// Filters for the contract-events stream. Exactly one of <see cref="ContractHash"/> or
    /// <see cref="ContractPackageHash"/> must be supplied.
    /// </summary>
    public class ContractEventStreamParameters : StreamParametersBase
    {
        /// <summary>Contract hashes to filter on (comma-joined by the client).</summary>
        public List<string> ContractHash { get; set; }

        /// <summary>Contract package hashes to filter on (comma-joined by the client).</summary>
        public List<string> ContractPackageHash { get; set; }

        /// <summary>
        /// When true, requests the hexadecimal raw event bytes via <c>includes=raw_data</c>.
        /// </summary>
        public bool IncludeRawData { get; set; }
    }

    /// <summary>
    /// Filters for the deploys stream.
    /// </summary>
    public class DeployStreamParameters : StreamParametersBase
    {
        /// <summary>Contract package hashes to filter on (comma-joined by the client).</summary>
        public List<string> ContractPackageHash { get; set; }

        /// <summary>Contract hashes to filter on (comma-joined by the client).</summary>
        public List<string> ContractHash { get; set; }

        /// <summary>Caller public keys to filter on (comma-joined by the client).</summary>
        public List<string> CallerPublicKey { get; set; }

        /// <summary>Contract entry-point IDs to filter on (comma-joined by the client).</summary>
        public List<string> ContractEntrypointId { get; set; }

        /// <summary>Deploy hashes to filter on (comma-joined by the client).</summary>
        public List<string> DeployHash { get; set; }
    }

    /// <summary>
    /// Filters for the ft-token-actions stream.
    /// </summary>
    public class FTTokenActionStreamParameters : StreamParametersBase
    {
        /// <summary>Contract package hashes to filter on (comma-joined by the client).</summary>
        public List<string> ContractPackageHash { get; set; }

        /// <summary>Owner account hashes to filter on (comma-joined by the client).</summary>
        public List<string> OwnerHash { get; set; }
    }

    /// <summary>
    /// Filters for the nft-tokens stream.
    /// </summary>
    public class NFTStreamParameters : StreamParametersBase
    {
        /// <summary>Contract package hashes to filter on (comma-joined by the client).</summary>
        public List<string> ContractPackageHash { get; set; }

        /// <summary>Owner account hashes to filter on (comma-joined by the client).</summary>
        public List<string> OwnerHash { get; set; }
    }

    /// <summary>
    /// Filters for the nft-token-actions stream.
    /// </summary>
    public class NFTActionStreamParameters : StreamParametersBase
    {
        /// <summary>Contract package hashes to filter on (comma-joined by the client).</summary>
        public List<string> ContractPackageHash { get; set; }

        /// <summary>Owner account hashes to filter on (comma-joined by the client).</summary>
        public List<string> OwnerHash { get; set; }
    }

    /// <summary>
    /// Filters for the transfers stream.
    /// </summary>
    public class TransferStreamParameters : StreamParametersBase
    {
        /// <summary>Account hashes to filter on (comma-joined by the client).</summary>
        public List<string> AccountHash { get; set; }

        /// <summary>Public keys to filter on (comma-joined by the client).</summary>
        public List<string> PublicKey { get; set; }
    }
}
