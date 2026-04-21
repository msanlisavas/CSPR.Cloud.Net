using CSPR.Cloud.Net.Clients.Api;
using CSPR.Cloud.Net.Clients.Socket;
using CSPR.Cloud.Net.Objects.Block;
using CSPR.Cloud.Net.Objects.Config;
using CSPR.Cloud.Net.Objects.Contract;
using CSPR.Cloud.Net.Objects.Deploy;
using CSPR.Cloud.Net.Objects.Ft;
using CSPR.Cloud.Net.Objects.Nft;
using CSPR.Cloud.Net.Objects.Socket;
using CSPR.Cloud.Net.Objects.Transfer;
using CSPR.Cloud.Net.Parameters.Socket;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace CSPR.Cloud.Net.Clients
{
    /// <summary>
    /// Exposes every CSPR Cloud streaming channel for a single network (Mainnet or Testnet).
    /// </summary>
    public interface INetworkSocketEndpoint
    {
        CasperCloudSocketClient.AccountBalanceStream AccountBalance { get; }
        CasperCloudSocketClient.BlockStream Block { get; }
        CasperCloudSocketClient.ContractStream Contract { get; }
        CasperCloudSocketClient.ContractPackageStream ContractPackage { get; }
        CasperCloudSocketClient.ContractEventStream ContractEvent { get; }
        CasperCloudSocketClient.DeployStream Deploy { get; }
        CasperCloudSocketClient.FTTokenActionStream FTTokenAction { get; }
        CasperCloudSocketClient.NFTStream NFT { get; }
        CasperCloudSocketClient.NFTActionStream NFTAction { get; }
        CasperCloudSocketClient.TransferStream Transfer { get; }
    }

    /// <summary>
    /// WebSocket-based client for the CSPR Cloud Streaming API.
    /// <para>
    /// Mirrors the shape of <see cref="CasperCloudRestClient"/>: pick a network via
    /// <see cref="Mainnet"/> or <see cref="Testnet"/>, pick a stream (e.g. <c>Block</c>),
    /// then call <c>SubscribeAsync(parameters, onMessage, cancellationToken)</c>.
    /// </para>
    /// <para>
    /// See <see href="https://docs.cspr.cloud/streaming-api/reference"/> for the wire protocol.
    /// </para>
    /// </summary>
    public class CasperCloudSocketClient
    {
        private readonly string _apiKey;
        private readonly string _persistentSessionId;
        private readonly ILogger _logger;
        private readonly Func<ClientWebSocket> _webSocketFactory;
        private readonly StreamReconnectPolicy _reconnectPolicy;

        /// <summary>
        /// Mainnet streaming endpoints. Typed as <see cref="INetworkSocketEndpoint"/> so callers can
        /// assign either network to the same variable:
        /// <code>var network = useTestnet ? socket.Testnet : socket.Mainnet;</code>
        /// </summary>
        public INetworkSocketEndpoint Mainnet { get; }

        /// <summary>
        /// Testnet streaming endpoints. Typed as <see cref="INetworkSocketEndpoint"/> so callers can
        /// assign either network to the same variable — see <see cref="Mainnet"/>.
        /// </summary>
        public INetworkSocketEndpoint Testnet { get; }

        /// <summary>
        /// Creates a new socket client.
        /// </summary>
        /// <param name="config">API key holder.</param>
        /// <param name="persistentSessionId">
        /// Optional <c>Persistent-Session</c> header value. Reconnecting with the same value
        /// replays messages that were queued while the client was offline (Beta, paid tiers only).
        /// </param>
        /// <param name="loggerFactory">Optional logger factory.</param>
        /// <param name="reconnectPolicy">
        /// Optional auto-reconnect policy. When <see cref="StreamReconnectPolicy.Enabled"/> is true,
        /// every subscription automatically reconnects with exponential backoff on transport failures
        /// and clean server disconnects. Pair with <paramref name="persistentSessionId"/> to replay
        /// queued messages across reconnects. Default: no reconnect (legacy single-attempt behavior).
        /// </param>
        /// <param name="webSocketFactory">
        /// Optional factory for the underlying <see cref="ClientWebSocket"/>. Exposed for tests.
        /// </param>
        public CasperCloudSocketClient(
            CasperCloudClientConfig config,
            string persistentSessionId = null,
            StreamReconnectPolicy reconnectPolicy = null,
            ILoggerFactory loggerFactory = null,
            Func<ClientWebSocket> webSocketFactory = null)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            _apiKey = config.ApiKey;
            _persistentSessionId = persistentSessionId;
            _reconnectPolicy = reconnectPolicy;
            _logger = loggerFactory?.CreateLogger<CasperCloudSocketClient>();
            _webSocketFactory = webSocketFactory;
            Mainnet = new MainnetSocketEndpoint(this);
            Testnet = new TestnetSocketEndpoint(this);
        }

        internal Task SubscribeInternalAsync<T>(
            Uri uri,
            Func<WebSocketMessage<T>, Task> onMessage,
            Func<Exception, Task> onError,
            Func<Exception, int, Task> onReconnecting,
            CancellationToken cancellationToken)
        {
            return SocketStreamHandler.SubscribeAsync(
                uri,
                _apiKey,
                _persistentSessionId,
                onMessage,
                onError,
                _webSocketFactory,
                _reconnectPolicy,
                onReconnecting,
                _logger,
                cancellationToken);
        }

        // ---------- Network endpoints ----------

        public class MainnetSocketEndpoint : NetworkSocketEndpointBase
        {
            public MainnetSocketEndpoint(CasperCloudSocketClient client)
                : base(client, Endpoints.BaseUrls.StreamingMainnet)
            {
            }
        }

        public class TestnetSocketEndpoint : NetworkSocketEndpointBase
        {
            public TestnetSocketEndpoint(CasperCloudSocketClient client)
                : base(client, Endpoints.BaseUrls.StreamingTestnet)
            {
            }
        }

        public abstract class NetworkSocketEndpointBase : INetworkSocketEndpoint
        {
            internal readonly CasperCloudSocketClient Client;
            internal readonly string BaseUrl;

            protected NetworkSocketEndpointBase(CasperCloudSocketClient client, string baseUrl)
            {
                Client = client;
                BaseUrl = baseUrl;
                AccountBalance = new AccountBalanceStream(this);
                Block = new BlockStream(this);
                Contract = new ContractStream(this);
                ContractPackage = new ContractPackageStream(this);
                ContractEvent = new ContractEventStream(this);
                Deploy = new DeployStream(this);
                FTTokenAction = new FTTokenActionStream(this);
                NFT = new NFTStream(this);
                NFTAction = new NFTActionStream(this);
                Transfer = new TransferStream(this);
            }

            public AccountBalanceStream AccountBalance { get; }
            public BlockStream Block { get; }
            public ContractStream Contract { get; }
            public ContractPackageStream ContractPackage { get; }
            public ContractEventStream ContractEvent { get; }
            public DeployStream Deploy { get; }
            public FTTokenActionStream FTTokenAction { get; }
            public NFTStream NFT { get; }
            public NFTActionStream NFTAction { get; }
            public TransferStream Transfer { get; }
        }

        // ---------- Streams ----------

        public class AccountBalanceStream
        {
            private readonly NetworkSocketEndpointBase _network;
            internal AccountBalanceStream(NetworkSocketEndpointBase network) { _network = network; }

            public Uri BuildUri(AccountBalanceStreamParameters parameters)
            {
                var filters = new List<KeyValuePair<string, IReadOnlyList<string>>>();
                if (parameters != null)
                {
                    filters.Add(StreamUrlBuilder.Filter("account_hash", parameters.AccountHash));
                    filters.Add(StreamUrlBuilder.Filter("public_key", parameters.PublicKey));
                }
                return StreamUrlBuilder.Build(_network.BaseUrl, Endpoints.BaseUrls.StreamAccountBalances, filters);
            }

            /// <summary>
            /// Subscribes to account-balance <c>updated</c> events. Runs until
            /// <paramref name="cancellationToken"/> is cancelled or the server closes the socket.
            /// </summary>
            public Task SubscribeAsync(
                AccountBalanceStreamParameters parameters,
                Func<WebSocketMessage<AccountBalanceStreamData>, Task> onMessage,
                Func<Exception, Task> onError = null,
                Func<Exception, int, Task> onReconnecting = null,
                CancellationToken cancellationToken = default)
            {
                return _network.Client.SubscribeInternalAsync(BuildUri(parameters), onMessage, onError, onReconnecting, cancellationToken);
            }
        }

        public class BlockStream
        {
            private readonly NetworkSocketEndpointBase _network;
            internal BlockStream(NetworkSocketEndpointBase network) { _network = network; }

            public Uri BuildUri(BlockStreamParameters parameters)
            {
                var filters = new List<KeyValuePair<string, IReadOnlyList<string>>>();
                if (parameters != null)
                {
                    filters.Add(StreamUrlBuilder.Filter("proposer_public_key", parameters.ProposerPublicKey));
                }
                return StreamUrlBuilder.Build(_network.BaseUrl, Endpoints.BaseUrls.StreamBlocks, filters);
            }

            /// <summary>
            /// Subscribes to block <c>created</c> events.
            /// </summary>
            public Task SubscribeAsync(
                BlockStreamParameters parameters,
                Func<WebSocketMessage<BlockData>, Task> onMessage,
                Func<Exception, Task> onError = null,
                Func<Exception, int, Task> onReconnecting = null,
                CancellationToken cancellationToken = default)
            {
                return _network.Client.SubscribeInternalAsync(BuildUri(parameters), onMessage, onError, onReconnecting, cancellationToken);
            }
        }

        public class ContractStream
        {
            private readonly NetworkSocketEndpointBase _network;
            internal ContractStream(NetworkSocketEndpointBase network) { _network = network; }

            public Uri BuildUri(ContractStreamParameters parameters)
            {
                var filters = new List<KeyValuePair<string, IReadOnlyList<string>>>();
                if (parameters != null)
                {
                    filters.Add(StreamUrlBuilder.Filter("contract_package_hash", parameters.ContractPackageHash));
                    filters.Add(StreamUrlBuilder.Filter("deploy_hash", parameters.DeployHash));
                }
                return StreamUrlBuilder.Build(_network.BaseUrl, Endpoints.BaseUrls.StreamContracts, filters);
            }

            /// <summary>
            /// Subscribes to contract <c>created</c> events.
            /// </summary>
            public Task SubscribeAsync(
                ContractStreamParameters parameters,
                Func<WebSocketMessage<ContractData>, Task> onMessage,
                Func<Exception, Task> onError = null,
                Func<Exception, int, Task> onReconnecting = null,
                CancellationToken cancellationToken = default)
            {
                return _network.Client.SubscribeInternalAsync(BuildUri(parameters), onMessage, onError, onReconnecting, cancellationToken);
            }
        }

        public class ContractPackageStream
        {
            private readonly NetworkSocketEndpointBase _network;
            internal ContractPackageStream(NetworkSocketEndpointBase network) { _network = network; }

            public Uri BuildUri(ContractPackageStreamParameters parameters)
            {
                var filters = new List<KeyValuePair<string, IReadOnlyList<string>>>();
                if (parameters != null)
                {
                    filters.Add(StreamUrlBuilder.Filter("contract_package_hash", parameters.ContractPackageHash));
                    filters.Add(StreamUrlBuilder.Filter("owner_public_key", parameters.OwnerPublicKey));
                }
                return StreamUrlBuilder.Build(_network.BaseUrl, Endpoints.BaseUrls.StreamContractPackages, filters);
            }

            /// <summary>
            /// Subscribes to contract-package <c>created</c> and <c>updated</c> events.
            /// </summary>
            public Task SubscribeAsync(
                ContractPackageStreamParameters parameters,
                Func<WebSocketMessage<ContractPackageData>, Task> onMessage,
                Func<Exception, Task> onError = null,
                Func<Exception, int, Task> onReconnecting = null,
                CancellationToken cancellationToken = default)
            {
                return _network.Client.SubscribeInternalAsync(BuildUri(parameters), onMessage, onError, onReconnecting, cancellationToken);
            }
        }

        public class ContractEventStream
        {
            private readonly NetworkSocketEndpointBase _network;
            internal ContractEventStream(NetworkSocketEndpointBase network) { _network = network; }

            public Uri BuildUri(ContractEventStreamParameters parameters)
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters),
                        "contract-events stream requires contract_hash or contract_package_hash.");
                }

                bool hasHash = parameters.ContractHash != null && parameters.ContractHash.Count > 0;
                bool hasPackageHash = parameters.ContractPackageHash != null && parameters.ContractPackageHash.Count > 0;
                if (!hasHash && !hasPackageHash)
                {
                    throw new ArgumentException(
                        "Provide either ContractHash or ContractPackageHash for the contract-events stream.",
                        nameof(parameters));
                }

                var filters = new List<KeyValuePair<string, IReadOnlyList<string>>>();
                filters.Add(StreamUrlBuilder.Filter("contract_hash", parameters.ContractHash));
                filters.Add(StreamUrlBuilder.Filter("contract_package_hash", parameters.ContractPackageHash));
                if (parameters.IncludeRawData)
                {
                    filters.Add(StreamUrlBuilder.Filter("includes", "raw_data"));
                }
                return StreamUrlBuilder.Build(_network.BaseUrl, Endpoints.BaseUrls.StreamContractEvents, filters);
            }

            /// <summary>
            /// Subscribes to contract-level <c>emitted</c> events.
            /// </summary>
            public Task SubscribeAsync(
                ContractEventStreamParameters parameters,
                Func<WebSocketMessage<ContractEventStreamData>, Task> onMessage,
                Func<Exception, Task> onError = null,
                Func<Exception, int, Task> onReconnecting = null,
                CancellationToken cancellationToken = default)
            {
                return _network.Client.SubscribeInternalAsync(BuildUri(parameters), onMessage, onError, onReconnecting, cancellationToken);
            }
        }

        public class DeployStream
        {
            private readonly NetworkSocketEndpointBase _network;
            internal DeployStream(NetworkSocketEndpointBase network) { _network = network; }

            public Uri BuildUri(DeployStreamParameters parameters)
            {
                var filters = new List<KeyValuePair<string, IReadOnlyList<string>>>();
                if (parameters != null)
                {
                    filters.Add(StreamUrlBuilder.Filter("contract_package_hash", parameters.ContractPackageHash));
                    filters.Add(StreamUrlBuilder.Filter("contract_hash", parameters.ContractHash));
                    filters.Add(StreamUrlBuilder.Filter("caller_public_key", parameters.CallerPublicKey));
                    filters.Add(StreamUrlBuilder.Filter("contract_entrypoint_id", parameters.ContractEntrypointId));
                    filters.Add(StreamUrlBuilder.Filter("deploy_hash", parameters.DeployHash));
                }
                return StreamUrlBuilder.Build(_network.BaseUrl, Endpoints.BaseUrls.StreamDeploys, filters);
            }

            /// <summary>
            /// Subscribes to deploy <c>created</c> events.
            /// </summary>
            public Task SubscribeAsync(
                DeployStreamParameters parameters,
                Func<WebSocketMessage<DeployData>, Task> onMessage,
                Func<Exception, Task> onError = null,
                Func<Exception, int, Task> onReconnecting = null,
                CancellationToken cancellationToken = default)
            {
                return _network.Client.SubscribeInternalAsync(BuildUri(parameters), onMessage, onError, onReconnecting, cancellationToken);
            }
        }

        public class FTTokenActionStream
        {
            private readonly NetworkSocketEndpointBase _network;
            internal FTTokenActionStream(NetworkSocketEndpointBase network) { _network = network; }

            public Uri BuildUri(FTTokenActionStreamParameters parameters)
            {
                var filters = new List<KeyValuePair<string, IReadOnlyList<string>>>();
                if (parameters != null)
                {
                    filters.Add(StreamUrlBuilder.Filter("contract_package_hash", parameters.ContractPackageHash));
                    filters.Add(StreamUrlBuilder.Filter("owner_hash", parameters.OwnerHash));
                }
                return StreamUrlBuilder.Build(_network.BaseUrl, Endpoints.BaseUrls.StreamFTTokenActions, filters);
            }

            /// <summary>
            /// Subscribes to fungible-token action <c>created</c> events.
            /// </summary>
            public Task SubscribeAsync(
                FTTokenActionStreamParameters parameters,
                Func<WebSocketMessage<FTTokenActionData>, Task> onMessage,
                Func<Exception, Task> onError = null,
                Func<Exception, int, Task> onReconnecting = null,
                CancellationToken cancellationToken = default)
            {
                return _network.Client.SubscribeInternalAsync(BuildUri(parameters), onMessage, onError, onReconnecting, cancellationToken);
            }
        }

        public class NFTStream
        {
            private readonly NetworkSocketEndpointBase _network;
            internal NFTStream(NetworkSocketEndpointBase network) { _network = network; }

            public Uri BuildUri(NFTStreamParameters parameters)
            {
                var filters = new List<KeyValuePair<string, IReadOnlyList<string>>>();
                if (parameters != null)
                {
                    filters.Add(StreamUrlBuilder.Filter("contract_package_hash", parameters.ContractPackageHash));
                    filters.Add(StreamUrlBuilder.Filter("owner_hash", parameters.OwnerHash));
                }
                return StreamUrlBuilder.Build(_network.BaseUrl, Endpoints.BaseUrls.StreamNFTTokens, filters);
            }

            /// <summary>
            /// Subscribes to NFT <c>created</c> and <c>updated</c> events.
            /// </summary>
            public Task SubscribeAsync(
                NFTStreamParameters parameters,
                Func<WebSocketMessage<NFTTokenData>, Task> onMessage,
                Func<Exception, Task> onError = null,
                Func<Exception, int, Task> onReconnecting = null,
                CancellationToken cancellationToken = default)
            {
                return _network.Client.SubscribeInternalAsync(BuildUri(parameters), onMessage, onError, onReconnecting, cancellationToken);
            }
        }

        public class NFTActionStream
        {
            private readonly NetworkSocketEndpointBase _network;
            internal NFTActionStream(NetworkSocketEndpointBase network) { _network = network; }

            public Uri BuildUri(NFTActionStreamParameters parameters)
            {
                var filters = new List<KeyValuePair<string, IReadOnlyList<string>>>();
                if (parameters != null)
                {
                    filters.Add(StreamUrlBuilder.Filter("contract_package_hash", parameters.ContractPackageHash));
                    filters.Add(StreamUrlBuilder.Filter("owner_hash", parameters.OwnerHash));
                }
                return StreamUrlBuilder.Build(_network.BaseUrl, Endpoints.BaseUrls.StreamNFTTokenActions, filters);
            }

            /// <summary>
            /// Subscribes to NFT action <c>created</c> events.
            /// </summary>
            public Task SubscribeAsync(
                NFTActionStreamParameters parameters,
                Func<WebSocketMessage<NFTTokenActionData>, Task> onMessage,
                Func<Exception, Task> onError = null,
                Func<Exception, int, Task> onReconnecting = null,
                CancellationToken cancellationToken = default)
            {
                return _network.Client.SubscribeInternalAsync(BuildUri(parameters), onMessage, onError, onReconnecting, cancellationToken);
            }
        }

        public class TransferStream
        {
            private readonly NetworkSocketEndpointBase _network;
            internal TransferStream(NetworkSocketEndpointBase network) { _network = network; }

            public Uri BuildUri(TransferStreamParameters parameters)
            {
                var filters = new List<KeyValuePair<string, IReadOnlyList<string>>>();
                if (parameters != null)
                {
                    filters.Add(StreamUrlBuilder.Filter("account_hash", parameters.AccountHash));
                    filters.Add(StreamUrlBuilder.Filter("public_key", parameters.PublicKey));
                }
                return StreamUrlBuilder.Build(_network.BaseUrl, Endpoints.BaseUrls.StreamTransfers, filters);
            }

            /// <summary>
            /// Subscribes to transfer <c>created</c> events.
            /// </summary>
            public Task SubscribeAsync(
                TransferStreamParameters parameters,
                Func<WebSocketMessage<TransferData>, Task> onMessage,
                Func<Exception, Task> onError = null,
                Func<Exception, int, Task> onReconnecting = null,
                CancellationToken cancellationToken = default)
            {
                return _network.Client.SubscribeInternalAsync(BuildUri(parameters), onMessage, onError, onReconnecting, cancellationToken);
            }
        }
    }
}
