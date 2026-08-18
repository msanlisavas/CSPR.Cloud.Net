using CSPR.Cloud.Net.Clients.Api;
using CSPR.Cloud.Net.Errors;
using CSPR.Cloud.Net.Objects.Abstract;
using CSPR.Cloud.Net.Objects.Account;
using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.Auction;
using CSPR.Cloud.Net.Objects.AwaitingDeploy;
using CSPR.Cloud.Net.Objects.Bidder;
using CSPR.Cloud.Net.Objects.Block;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using CSPR.Cloud.Net.Objects.Config;
using CSPR.Cloud.Net.Objects.Contract;
using CSPR.Cloud.Net.Objects.CsprName;
using CSPR.Cloud.Net.Objects.Delegate;
using CSPR.Cloud.Net.Objects.Deploy;
using CSPR.Cloud.Net.Objects.Dex;
using CSPR.Cloud.Net.Objects.Ft;
using CSPR.Cloud.Net.Objects.Nft;
using CSPR.Cloud.Net.Objects.Rate;
using CSPR.Cloud.Net.Objects.Supply;
using CSPR.Cloud.Net.Objects.Swap;
using CSPR.Cloud.Net.Objects.Transfer;
using CSPR.Cloud.Net.Objects.Validator;
using CSPR.Cloud.Net.Parameters.Filtering.Ft;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Account;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Block;
using CSPR.Cloud.Net.Parameters.Wrapper.Accounts;
using CSPR.Cloud.Net.Parameters.Wrapper.Bidder;
using CSPR.Cloud.Net.Parameters.Wrapper.Block;
using CSPR.Cloud.Net.Parameters.Wrapper.CentralizedAccountInfo;
using CSPR.Cloud.Net.Parameters.Wrapper.Contract;
using CSPR.Cloud.Net.Parameters.Wrapper.Delegate;
using CSPR.Cloud.Net.Parameters.Wrapper.Deploy;
using CSPR.Cloud.Net.Parameters.Wrapper.Ft;
using CSPR.Cloud.Net.Parameters.Wrapper.Nft;
using CSPR.Cloud.Net.Parameters.Wrapper.Rate;
using CSPR.Cloud.Net.Parameters.Wrapper.Swap;
using CSPR.Cloud.Net.Parameters.Wrapper.Transfer;
using CSPR.Cloud.Net.Parameters.Wrapper.Validator;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSPR.Cloud.Net.Clients
{
    public interface INetworkEndpoint
    {
        CasperCloudRestClient.Account Account { get; }
        CasperCloudRestClient.Auction Auction { get; }
        CasperCloudRestClient.AwaitingDeployEndpoint AwaitingDeploy { get; }
        CasperCloudRestClient.Bidder Bidder { get; }
        CasperCloudRestClient.Block Block { get; }
        CasperCloudRestClient.CentralizedAccount CentralizedAccount { get; }
        CasperCloudRestClient.Contract Contract { get; }
        CasperCloudRestClient.CsprNameEndpoint CsprName { get; }
        CasperCloudRestClient.Delegate Delegate { get; }
        CasperCloudRestClient.Deploy Deploy { get; }
        CasperCloudRestClient.DexEndpoint Dex { get; }
        CasperCloudRestClient.FT FT { get; }
        CasperCloudRestClient.NFT NFT { get; }
        CasperCloudRestClient.Rate Rate { get; }
        CasperCloudRestClient.Supply Supply { get; }
        CasperCloudRestClient.SwapEndpoint Swap { get; }
        CasperCloudRestClient.Transfer Transfer { get; }
        CasperCloudRestClient.Validator Validator { get; }
    }

    public class CasperCloudRestClient
    {
        private readonly string _apiKey;

        private readonly HttpClient _httpClient;

        private readonly ILogger? _logger;

        private readonly bool _tolerateMalformedRows;

        public INetworkEndpoint Mainnet { get; }
        public INetworkEndpoint Testnet { get; }
        // Primary constructor
        public CasperCloudRestClient(CasperCloudClientConfig config, HttpClient? httpClient = null, ILoggerFactory? loggerFactory = null)
        {
            _apiKey = config.ApiKey;
            _tolerateMalformedRows = config.TolerateMalformedRows;
            _httpClient = httpClient ?? new HttpClient();
            _logger = loggerFactory?.CreateLogger<CasperCloudRestClient>();
            Mainnet = new MainnetEndpoint(this);
            Testnet = new TestnetEndpoint(this);
        }


        private async Task<T?> HandleResponseAsync<T>(HttpResponseMessage response) where T : class
        {
            var code = response.StatusCode;
            if (code == HttpStatusCode.OK || code == HttpStatusCode.Created)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(content)) return null;
                return Deserialize<T>(content);
            }
            if (code == HttpStatusCode.NotFound) return null;

            var body = await response.Content.ReadAsStringAsync();
            switch (code)
            {
                case HttpStatusCode.BadRequest: throw new InvalidParamException($"Invalid Param Error: {body}", _logger);
                case HttpStatusCode.Unauthorized: throw new UnauthorizedException($"Unauthorized Error: {body}", _logger);
                case HttpStatusCode.Forbidden: throw new AccessDeniedException($"Access Denied Error: {body}", _logger);
                case HttpStatusCode.Conflict: throw new DuplicateEntryException($"Duplicate Entry Error: {body}", _logger);
                case (HttpStatusCode)429: throw new RateLimitException($"Rate Limit Error: {body}", _logger);
            }
            if ((int)code >= 500) throw new InternalServerErrorException($"Server Error ({(int)code}): {body}", _logger);
            throw new HttpRequestException($"Unexpected response status {(int)code} ({code}): {body}");
        }

        /// <summary>
        /// Deserializes a successful response body. With
        /// <see cref="CasperCloudClientConfig.TolerateMalformedRows"/> off this is a plain
        /// <see cref="JsonConvert.DeserializeObject{T}(string)"/> call and any bad row throws, as
        /// before. With it on, the envelope is deserialized with its rows detached and the rows are
        /// then converted one at a time, so a row that fails costs only itself while a malformed
        /// envelope still throws — a response whose envelope is broken can't be trusted at all.
        /// </summary>
        private T? Deserialize<T>(string content) where T : class
        {
            if (!_tolerateMalformedRows) return JsonConvert.DeserializeObject<T>(content);

            // Only envelopes with a data array can lose a row without losing the response. Anything
            // else (a single object, a bare value) deserializes exactly as it always has.
            if (!(ParseDecimalPreserving(content) is JObject envelope) || !(envelope["data"] is JArray rows))
                return JsonConvert.DeserializeObject<T>(content);

            // Deserialize the envelope with the rows detached, so a broken envelope still throws —
            // tolerating rows must not quietly downgrade an untrustworthy response into a partial one.
            envelope["data"] = new JArray();
            var serializer = JsonSerializer.CreateDefault();
            serializer.FloatParseHandling = FloatParseHandling.Decimal;
            var result = envelope.ToObject<T>(serializer);

            if (result is ISkipTolerantResponse tolerant)
                tolerant.SkippedItemCount = tolerant.PopulateRows(rows, serializer);

            return result;
        }

        /// <summary>
        /// Parses a payload into a <see cref="JToken"/> WITHOUT collapsing JSON numbers to
        /// <see cref="double"/> first.
        /// <para>Newtonsoft defaults to <see cref="FloatParseHandling.Double"/>, so a plain
        /// <c>JToken.Parse</c> would round every rate and amount to double precision on the way into
        /// the tree, and the later conversion to <c>decimal</c> could only preserve the already-lost
        /// value — silently undoing the decimal typing this library exists to guarantee. Only the
        /// tolerant path builds a JToken at all, which is why the defect was invisible on the direct
        /// deserialize path.</para>
        /// </summary>
        private static JToken ParseDecimalPreserving(string content)
        {
            using (var reader = new JsonTextReader(new StringReader(content))
            {
                FloatParseHandling = FloatParseHandling.Decimal
            })
            {
                return JToken.ReadFrom(reader);
            }
        }

        public async Task<T?> GetDataAsync<T>(string endpoint, CancellationToken cancellationToken = default) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{endpoint}");
            request.Headers.Add("Authorization", _apiKey);
            request.Headers.Add("Accept", "application/json");

            var response = await _httpClient.SendAsync(request, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        public async Task<T?> PostDataAsync<T>(string endpoint, object body, CancellationToken cancellationToken = default) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{endpoint}");
            request.Headers.Add("Authorization", _apiKey);
            request.Headers.Add("Accept", "application/json");
            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        public class MainnetEndpoint : INetworkEndpoint
        {
            private readonly CommonEndpoint _commonEndpoint;
            public Account Account { get; }
            public Auction Auction { get; }
            public AwaitingDeployEndpoint AwaitingDeploy { get; }
            public Block Block { get; }
            public Bidder Bidder { get; }
            public CentralizedAccount CentralizedAccount { get; }
            public Contract Contract { get; }
            public CsprNameEndpoint CsprName { get; }
            public Delegate Delegate { get; }
            public Deploy Deploy { get; }
            public DexEndpoint Dex { get; }
            public FT FT { get; }
            public NFT NFT { get; }
            public Rate Rate { get; }
            public Supply Supply { get; }
            public SwapEndpoint Swap { get; }
            public Transfer Transfer { get; }
            public Validator Validator { get; }
            public MainnetEndpoint(CasperCloudRestClient casperCloudRestClient)
            {
                _commonEndpoint = new CommonEndpoint(casperCloudRestClient, Endpoints.BaseUrls.Mainnet);
                Account = new Account(_commonEndpoint);
                Auction = new Auction(_commonEndpoint);
                AwaitingDeploy = new AwaitingDeployEndpoint(_commonEndpoint);
                Block = new Block(_commonEndpoint);
                Bidder = new Bidder(_commonEndpoint);
                CentralizedAccount = new CentralizedAccount(_commonEndpoint);
                Contract = new Contract(_commonEndpoint);
                CsprName = new CsprNameEndpoint(_commonEndpoint);
                Delegate = new Delegate(_commonEndpoint);
                Deploy = new Deploy(_commonEndpoint);
                Dex = new DexEndpoint(_commonEndpoint);
                FT = new FT(_commonEndpoint);
                NFT = new NFT(_commonEndpoint);
                Rate = new Rate(_commonEndpoint);
                Supply = new Supply(_commonEndpoint);
                Swap = new SwapEndpoint(_commonEndpoint);
                Transfer = new Transfer(_commonEndpoint);
                Validator = new Validator(_commonEndpoint);
            }

        }
        public class TestnetEndpoint : INetworkEndpoint
        {
            private readonly CommonEndpoint _commonEndpoint;
            public Account Account { get; }
            public Auction Auction { get; }
            public AwaitingDeployEndpoint AwaitingDeploy { get; }
            public Block Block { get; }
            public Bidder Bidder { get; }
            public CentralizedAccount CentralizedAccount { get; }
            public Contract Contract { get; }
            public CsprNameEndpoint CsprName { get; }
            public Delegate Delegate { get; }
            public Deploy Deploy { get; }
            public DexEndpoint Dex { get; }
            public FT FT { get; }
            public NFT NFT { get; }
            public Rate Rate { get; }
            public Supply Supply { get; }
            public SwapEndpoint Swap { get; }
            public Transfer Transfer { get; }
            public Validator Validator { get; }
            public TestnetEndpoint(CasperCloudRestClient casperCloudRestClient)
            {
                _commonEndpoint = new CommonEndpoint(casperCloudRestClient, Endpoints.BaseUrls.Testnet);
                Account = new Account(_commonEndpoint);
                Auction = new Auction(_commonEndpoint);
                AwaitingDeploy = new AwaitingDeployEndpoint(_commonEndpoint);
                Block = new Block(_commonEndpoint);
                Bidder = new Bidder(_commonEndpoint);
                CentralizedAccount = new CentralizedAccount(_commonEndpoint);
                Contract = new Contract(_commonEndpoint);
                CsprName = new CsprNameEndpoint(_commonEndpoint);
                Delegate = new Delegate(_commonEndpoint);
                Deploy = new Deploy(_commonEndpoint);
                Dex = new DexEndpoint(_commonEndpoint);
                FT = new FT(_commonEndpoint);
                NFT = new NFT(_commonEndpoint);
                Rate = new Rate(_commonEndpoint);
                Supply = new Supply(_commonEndpoint);
                Swap = new SwapEndpoint(_commonEndpoint);
                Transfer = new Transfer(_commonEndpoint);
                Validator = new Validator(_commonEndpoint);
            }
        }

        public class CommonEndpoint
        {
            private readonly CasperCloudRestClient _casperCloudRestClient;
            private readonly string _baseUrl;

            public CommonEndpoint(CasperCloudRestClient casperCloudRestClient, string baseUrl)
            {
                _casperCloudRestClient = casperCloudRestClient;
                _baseUrl = baseUrl;
            }

            public async Task<AccountData?> GetAccountAsync(string publicKey, AccountsOptionalParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Account.GetAccount(_baseUrl, publicKey, parameters);
                var response = await _casperCloudRestClient.GetDataAsync<Response<AccountData>>(endpoint, cancellationToken);
                return response?.Data;
            }
            public async Task<PaginatedResponse<AccountData>?> GetAccountsAsync(AccountsRequestParameters parameters, CancellationToken cancellationToken = default)
            {

                string endpoint = Endpoints.Account.GetAccounts(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<AccountData>>(endpoint, cancellationToken);
            }
            public async Task<BlockData?> GetBlockAsync(string blockHash, BlockOptionalParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Block.GetBlock(_baseUrl, blockHash, parameters);
                var response = await _casperCloudRestClient.GetDataAsync<Response<BlockData>>(endpoint, cancellationToken);
                return response?.Data;
            }
            public async Task<PaginatedResponse<BlockData>?> GetBlocksAsync(BlockRequestParameters parameters, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Block.GetBlocks(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<BlockData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<BlockData>?> GetValidatorBlocksAsync(string validatorPublicKey, BlockRequestParameters parameters, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Block.GetValidatorBlocks(_baseUrl, validatorPublicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<BlockData>>(endpoint, cancellationToken);
            }
            public async Task<BidderData?> GetBidderAsync(string publicKey, BidderRequestParameters parameters, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Bidder.GetBidder(_baseUrl, publicKey, parameters);
                var response = await _casperCloudRestClient.GetDataAsync<Response<BidderData>>(endpoint, cancellationToken);
                return response?.Data;
            }
            public async Task<PaginatedResponse<BidderData>?> GetBiddersAsync(BiddersRequestParameters parameters, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Bidder.GetBidders(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<BidderData>>(endpoint, cancellationToken);
            }
            public async Task<CentralizedAccountInfoData?> GetCentralizedAccountInfoAsync(string accountHash, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.CentralizedAccountInfo.GetCentralizedAccountInfo(_baseUrl, accountHash);
                var response = await _casperCloudRestClient.GetDataAsync<Response<CentralizedAccountInfoData>>(endpoint, cancellationToken);
                return response?.Data;
            }
            public async Task<PaginatedResponse<CentralizedAccountInfoData>?> GetCentralizedAccountInfosAsync(CentralizedAccountInfoRequestParameters parameters, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.CentralizedAccountInfo.GetCentralizedInfos(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<CentralizedAccountInfoData>>(endpoint, cancellationToken);
            }
            public async Task<ContractData?> GetContractAsync(string contractHash, ContractRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Contract.GetContract(_baseUrl, contractHash, parameters);
                var response = await _casperCloudRestClient.GetDataAsync<Response<ContractData>>(endpoint, cancellationToken);
                return response?.Data;
            }
            public async Task<PaginatedResponse<ContractData>?> GetContractsAsync(ContractsRequestParameters parameters, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Contract.GetContracts(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<ContractData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<ContractData>?> GetContractsByContractPackageAsync(string contractPackageHash, ByContractRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Contract.GetContractsByContractPackage(_baseUrl, contractPackageHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<ContractData>>(endpoint, cancellationToken);
            }
            public async Task<List<ContractTypeData>?> GetContractTypesAsync(CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Contract.GetContractTypes(_baseUrl);
                var response = await _casperCloudRestClient.GetDataAsync<Response<List<ContractTypeData>>>(endpoint, cancellationToken);
                return response?.Data;
            }
            public async Task<PaginatedResponse<EntryPointData>?> GetContractEntryPointsAsync(string contractHash, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Contract.GetContractEntryPoints(_baseUrl, contractHash);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<EntryPointData>>(endpoint, cancellationToken);
            }
            public async Task<Response<EntryPointCostData>?> GetContractEntryPointCostsAsync(string contractHash, string entryPointName, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Contract.GetContractEntryPointCosts(_baseUrl, contractHash, entryPointName);
                return await _casperCloudRestClient.GetDataAsync<Response<EntryPointCostData>>(endpoint, cancellationToken);
            }
            public async Task<ContractResponse<ContractPackageData>?> GetContractPackageAsync(string contractPackageHash, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Contract.GetContractPackage(_baseUrl, contractPackageHash);
                return await _casperCloudRestClient.GetDataAsync<ContractResponse<ContractPackageData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<ContractPackageData>?> GetContractPackagesAsync(ContractPackageRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Contract.GetContractPackages(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<ContractPackageData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<ContractPackageData>?> GetAccountContractPackagesAsync(string publicKey, AccountContractPackageRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Contract.GetAccountContractPackages(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<ContractPackageData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<DelegationData>?> GetAccountDelegationsAsync(string publicKey, DelegationRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Delegate.GetAccountDelegations(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<DelegationData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<UndelegationData>?> GetAccountUndelegationsAsync(string publicKey, DelegationRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Delegate.GetAccountUndelegations(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<UndelegationData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<DelegationData>?> GetValidatorDelegationsAsync(string publicKey, DelegationRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Delegate.GetValidatorDelegations(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<DelegationData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<DelegatorRewardData>?> GetAccountDelegatorRewardsAsync(string publicKey, AccountDelegatorRewardRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Delegate.GetAccountDelegatorRewards(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<DelegatorRewardData>>(endpoint, cancellationToken);
            }
            public async Task<ulong> GetTotalAccountDelegationRewards(string publicKey, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Delegate.GetTotalAccountDelegationRewards(_baseUrl, publicKey);
                var response = await _casperCloudRestClient.GetDataAsync<Response<ulong>>(endpoint, cancellationToken);
                return response?.Data ?? 0;
            }
            public async Task<ulong> GetTotalValidatorDelegationRewards(string publicKey, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Delegate.GetTotalValidatorDelegatorsRewards(_baseUrl, publicKey);
                var response = await _casperCloudRestClient.GetDataAsync<Response<ulong>>(endpoint, cancellationToken);
                return response?.Data ?? 0;
            }
            public async Task<Response<DeployData>?> GetDeployAsync(string deployHash, DeployRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Deploy.GetDeploy(_baseUrl, deployHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<Response<DeployData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<DeployData>?> GetDeploysAsync(DeploysRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Deploy.GetDeploys(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<DeployData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<DeployData>?> GetAccountDeploysAsync(string publicKey, AccountDeploysRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Deploy.GetAccountDeploys(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<DeployData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<DeployData>?> GetBlockDeploysAsync(string blockIdentifier, BlockDeploysRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Deploy.GetBlockDeploys(_baseUrl, blockIdentifier, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<DeployData>>(endpoint, cancellationToken);
            }
            public async Task<Response<List<DeployExecutionTypesData>>?> GetDeployExecutionTypesAsync(CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Deploy.GetDeployExecutionTypes(_baseUrl);
                return await _casperCloudRestClient.GetDataAsync<Response<List<DeployExecutionTypesData>>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<FTTokenActionData>?> GetFungibleTokenActionsAsync(FTActionRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.FT.GetFungibleTokenActions(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<FTTokenActionData>>(endpoint, cancellationToken);

            }
            public async Task<PaginatedResponse<FTTokenActionData>?> GetAccountFungibleTokenActionsAsync(string accountIdentifier, FTAccountActionRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.FT.GetAccountFungibleTokenActions(_baseUrl, accountIdentifier, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<FTTokenActionData>>(endpoint, cancellationToken);

            }
            public async Task<PaginatedResponse<FTTokenActionData>?> GetContractPackageFungibleTokenActionsAsync(string contractPackageHash, FTContractPackageActionRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.FT.GetContractPackageFungibleTokenActions(_baseUrl, contractPackageHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<FTTokenActionData>>(endpoint, cancellationToken);

            }
            public async Task<PaginatedResponse<FTOwnershipData>?> GetAccountFungibleTokenOwnershipAsync(string accountIdentifier, FTAccountOwnershipRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.FT.GetAccountFungibleTokenOwnership(_baseUrl, accountIdentifier, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<FTOwnershipData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<FTOwnershipData>?> GetContractPackageFungibleTokenOwnershipAsync(string contractPackageHash, FTContractPackageOwnershipRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.FT.GetContractPackageFungibleTokenOwnership(_baseUrl, contractPackageHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<FTOwnershipData>>(endpoint, cancellationToken);
            }
            public async Task<Response<NFTTokenData>?> GetNFTAsync(string contractPackageHash, string tokenId, NFTRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.NFT.GetNonFungibleToken(_baseUrl, contractPackageHash, tokenId, parameters);
                return await _casperCloudRestClient.GetDataAsync<Response<NFTTokenData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<NFTTokenData>?> GetNFTsAsync(NFTsRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.NFT.GetNFTs(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<NFTTokenData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<NFTTokenData>?> GetAccountNFTsAsync(string accountIdentifier, NFTAccountRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.NFT.GetAccountNFTs(_baseUrl, accountIdentifier, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<NFTTokenData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<NFTTokenData>?> GetContractPackageNFTsAsync(string contractPackageHash, NFTContractPackageRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.NFT.GetContractPackageNFTs(_baseUrl, contractPackageHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<NFTTokenData>>(endpoint, cancellationToken);
            }
            public async Task<ListResponse<NFTStandardData>?> GetNFTStandardsAsync(CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.NFT.GetNFTStandards(_baseUrl);
                return await _casperCloudRestClient.GetDataAsync<ListResponse<NFTStandardData>>(endpoint, cancellationToken);
            }
            public async Task<ListResponse<NFTMetadataStatusData>?> GetOffchainNFTMetadataStatusesAsync(CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.NFT.GetOffchainNFTMetadataStatuses(_baseUrl);
                return await _casperCloudRestClient.GetDataAsync<ListResponse<NFTMetadataStatusData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<NFTTokenActionData>?> GetContractPackageNFTActionsForATokenAsync(string contractPackageHash, string tokenId, NFTContractPackageTokenActionsRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.NFT.GetContractPackageActionsForAToken(_baseUrl, contractPackageHash, tokenId, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<NFTTokenActionData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<NFTTokenActionData>?> GetAccountNFTActionsAsync(string accountIdentifier, NFTAccountActionsRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.NFT.GetAccountNFTActions(_baseUrl, accountIdentifier, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<NFTTokenActionData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<NFTTokenActionData>?> GetContractPackageNFTActionsAsync(string contractPackageHash, NFTContractPackageActionsRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.NFT.GetContractPackageNFTActions(_baseUrl, contractPackageHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<NFTTokenActionData>>(endpoint, cancellationToken);
            }
            public async Task<ListResponse<NFTActionTypesData>?> GetNFTActionTypesAsync(CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.NFT.GetNFTActionTypes(_baseUrl);
                return await _casperCloudRestClient.GetDataAsync<ListResponse<NFTActionTypesData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<NFTTokenOwnershipData>?> GetContractPackageNFTOwnershipAsync(string contractPackageHash, NFTContractPackageOwnershipRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.NFT.GetContractPackageNFTOwnership(_baseUrl, contractPackageHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<NFTTokenOwnershipData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<NFTTokenOwnershipData>?> GetAccountNFTOwnershipAsync(string accountIdentifier, NFTAccountOwnershipRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.NFT.GetAccountNFTOwnership(_baseUrl, accountIdentifier, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<NFTTokenOwnershipData>>(endpoint, cancellationToken);
            }
            public async Task<Response<RateData>?> GetCurrentCurrencyRateAsync(string currencyId, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Rate.GetCurrentCurrencyRate(_baseUrl, currencyId);
                return await _casperCloudRestClient.GetDataAsync<Response<RateData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<RateData>?> GetHistoricalCurrencyRatesAsync(string currencyId, RateHistoricalRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Rate.GetHistoricalCurrencyRates(_baseUrl, currencyId, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<RateData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<CurrencyData>?> GetCurrenciesAsync(RateCurrenciesRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Rate.GetCurrencies(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<CurrencyData>>(endpoint, cancellationToken);
            }
            public async Task<Response<SupplyData>?> GetSupplyAsync(CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Supply.GetSupply(_baseUrl);
                return await _casperCloudRestClient.GetDataAsync<Response<SupplyData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<TransferData>?> GetAccountTransfersAsync(string accountIdentifier, TransferAccountRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Transfer.GetAccountTransfers(_baseUrl, accountIdentifier, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<TransferData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<TransferData>?> GetDeployTransfersAsync(string deployHash, TransferDeployRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Transfer.GetDeployTransfers(_baseUrl, deployHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<TransferData>>(endpoint, cancellationToken);
            }
            public async Task<Response<ValidatorData>?> GetValidatorAsync(string publicKey, ValidatorRequestParameters parameters, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Validator.GetValidator(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<Response<ValidatorData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<ValidatorData>?> GetValidatorsAsync(ValidatorsRequestParameters parameters, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Validator.GetValidators(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<ValidatorData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<RelativeValidatorPerformanceData>?> GetHistoricalValidatorPerformanceAsync(string publicKey, ValidatorHistoricalPerformanceRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Validator.GetHistoricalValidatorPerformance(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<RelativeValidatorPerformanceData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<ValidatorPerformanceData>?> GetHistoricalValidatorAveragePerformanceAsync(string publicKey, ValidatorHistoricalAveragePerformanceRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Validator.GetHistoricalAverageValidatorPerformance(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<ValidatorPerformanceData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<RelativeValidatorPerformanceData>?> GetHistoricalValidatorsAveragePerformanceAsync(ValidatorsHistoricalAveragePerformanceRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Validator.GetHistoricalAverageValidatorsPerformance(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<RelativeValidatorPerformanceData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<ValidatorRewardData>?> GetValidatorRewardsAsync(string publicKey, ValidatorRewardsRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Validator.GetValidatorRewards(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<ValidatorRewardData>>(endpoint, cancellationToken);
            }
            public async Task<Response<ulong>?> GetValidatorTotalRewardsAsync(string publicKey, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Validator.GetValidatorTotalRewards(_baseUrl, publicKey);
                return await _casperCloudRestClient.GetDataAsync<Response<ulong>>(endpoint, cancellationToken);
            }
            public async Task<Response<AccountInfoData>?> GetAccountInfoAsync(string accountHash, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Account.GetAccountInfo(_baseUrl, accountHash);
                return await _casperCloudRestClient.GetDataAsync<Response<AccountInfoData>>(endpoint, cancellationToken);
            }
            public async Task<PaginatedResponse<AccountInfoData>?> GetAccountInfosAsync(AccountInfosRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Account.GetAccountInfos(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<AccountInfoData>>(endpoint, cancellationToken);
            }
            public async Task<Response<AuctionMetricsData>?> GetAuctionMetricsAsync(CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Auction.GetAuctionMetrics(_baseUrl);
                return await _casperCloudRestClient.GetDataAsync<Response<AuctionMetricsData>>(endpoint, cancellationToken);
            }
            // DEX
            public async Task<ListResponse<DexData>?> GetDexesAsync(CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Dex.GetDexes(_baseUrl);
                return await _casperCloudRestClient.GetDataAsync<ListResponse<DexData>>(endpoint, cancellationToken);
            }
            // FT Action Types
            public async Task<ListResponse<FTActionTypeData>?> GetFTTokenActionTypesAsync(CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.FT.GetFTTokenActionTypes(_baseUrl);
                return await _casperCloudRestClient.GetDataAsync<ListResponse<FTActionTypeData>>(endpoint, cancellationToken);
            }
            // CSPR.name Resolution
            public async Task<CsprNameResolutionData?> GetCsprNameResolutionAsync(string name, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.CsprName.GetCsprNameResolution(_baseUrl, name);
                var response = await _casperCloudRestClient.GetDataAsync<Response<CsprNameResolutionData>>(endpoint, cancellationToken);
                return response?.Data;
            }
            // Purse Transfers
            public async Task<PaginatedResponse<TransferData>?> GetPurseTransfersAsync(string purseUref, TransferAccountRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Transfer.GetPurseTransfers(_baseUrl, purseUref, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<TransferData>>(endpoint, cancellationToken);
            }
            // Purse Delegations
            public async Task<PaginatedResponse<DelegationData>?> GetPurseDelegationsAsync(string purseUref, DelegationRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Delegate.GetPurseDelegations(_baseUrl, purseUref, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<DelegationData>>(endpoint, cancellationToken);
            }
            // Purse Delegation Rewards
            public async Task<PaginatedResponse<DelegatorRewardData>?> GetPurseDelegationRewardsAsync(string purseUref, AccountDelegatorRewardRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Delegate.GetPurseDelegationRewards(_baseUrl, purseUref, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<DelegatorRewardData>>(endpoint, cancellationToken);
            }
            public async Task<ulong> GetTotalPurseDelegationRewardsAsync(string purseUref, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Delegate.GetTotalPurseDelegationRewards(_baseUrl, purseUref);
                var response = await _casperCloudRestClient.GetDataAsync<Response<ulong>>(endpoint, cancellationToken);
                return response?.Data ?? 0;
            }
            // Validator Era Rewards
            public async Task<PaginatedResponse<ValidatorRewardData>?> GetValidatorEraRewardsAsync(string publicKey, ValidatorEraRewardsRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Validator.GetValidatorEraRewards(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<ValidatorRewardData>>(endpoint, cancellationToken);
            }
            // FT Rate endpoints
            public async Task<FTRateData?> GetFTRateLatestAsync(string contractPackageHash, FTRateFilterParameters filterParameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.FT.GetFTRateLatest(_baseUrl, contractPackageHash, filterParameters);
                var response = await _casperCloudRestClient.GetDataAsync<Response<FTRateData>>(endpoint, cancellationToken);
                return response?.Data;
            }
            public async Task<PaginatedResponse<FTRateData>?> GetFTRatesAsync(string contractPackageHash, FTRateRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.FT.GetFTRates(_baseUrl, contractPackageHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<FTRateData>>(endpoint, cancellationToken);
            }
            public async Task<FTDailyRateData?> GetFTDailyRateLatestAsync(string contractPackageHash, FTRateFilterParameters filterParameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.FT.GetFTDailyRateLatest(_baseUrl, contractPackageHash, filterParameters);
                var response = await _casperCloudRestClient.GetDataAsync<Response<FTDailyRateData>>(endpoint, cancellationToken);
                return response?.Data;
            }
            public async Task<PaginatedResponse<FTDailyRateData>?> GetFTDailyRatesAsync(string contractPackageHash, FTDailyRateRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.FT.GetFTDailyRates(_baseUrl, contractPackageHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<FTDailyRateData>>(endpoint, cancellationToken);
            }
            public async Task<FTDexRateData?> GetFTDexRateLatestAsync(string contractPackageHash, FTDexRateFilterParameters filterParameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.FT.GetFTDexRateLatest(_baseUrl, contractPackageHash, filterParameters);
                var response = await _casperCloudRestClient.GetDataAsync<Response<FTDexRateData>>(endpoint, cancellationToken);
                return response?.Data;
            }
            public async Task<PaginatedResponse<FTDexRateData>?> GetFTDexRatesAsync(string contractPackageHash, FTDexRateRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.FT.GetFTDexRates(_baseUrl, contractPackageHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<FTDexRateData>>(endpoint, cancellationToken);
            }
            public async Task<FTDailyDexRateData?> GetFTDailyDexRateLatestAsync(string contractPackageHash, FTDexRateFilterParameters filterParameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.FT.GetFTDailyDexRateLatest(_baseUrl, contractPackageHash, filterParameters);
                var response = await _casperCloudRestClient.GetDataAsync<Response<FTDailyDexRateData>>(endpoint, cancellationToken);
                return response?.Data;
            }
            public async Task<PaginatedResponse<FTDailyDexRateData>?> GetFTDailyDexRatesAsync(string contractPackageHash, FTDailyDexRateRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.FT.GetFTDailyDexRates(_baseUrl, contractPackageHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<FTDailyDexRateData>>(endpoint, cancellationToken);
            }
            // Swap
            public async Task<PaginatedResponse<SwapData>?> GetSwapsAsync(SwapRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.Swap.GetSwaps(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<SwapData>>(endpoint, cancellationToken);
            }
            // Awaiting Deploy
            public async Task<Response<bool>?> CreateAwaitingDeployAsync(CreateAwaitingDeployRequest request, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.AwaitingDeploy.CreateAwaitingDeploy(_baseUrl);
                return await _casperCloudRestClient.PostDataAsync<Response<bool>>(endpoint, request.Deploy, cancellationToken);
            }
            public async Task<Response<bool>?> AddAwaitingDeployApprovalsAsync(string deployHash, AddApprovalRequest request, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.AwaitingDeploy.AddAwaitingDeployApprovals(_baseUrl, deployHash);
                return await _casperCloudRestClient.PostDataAsync<Response<bool>>(endpoint, request, cancellationToken);
            }
            public async Task<AwaitingDeployData?> GetAwaitingDeployAsync(string deployHash, CancellationToken cancellationToken = default)
            {
                string endpoint = Endpoints.AwaitingDeploy.GetAwaitingDeploy(_baseUrl, deployHash);
                return await _casperCloudRestClient.GetDataAsync<AwaitingDeployData>(endpoint, cancellationToken);
            }
        }
        /// <summary>
        /// Represents the Account endpoints of the CSPR.Cloud API.
        /// </summary>
        public class Account
        {
            private readonly CommonEndpoint _commonEndpoint;

            public Account(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }
            /// <summary>
            /// Get account by its identifier (public key or account hash)
            /// <para>For more information, see <see href="https://docs.cspr.cloud/rest-api/account/get-account">CSPR.Cloud API documentation</see>.</para>
            /// </summary>
            /// <param name="publicKey">The public key of the account.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>The account data.</returns>
            public Task<AccountData?> GetAccountAsync(string publicKey, AccountsOptionalParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAccountAsync(publicKey, parameters, cancellationToken);
            }
            /// <summary>
            /// Gets a paginated list of accounts
            /// <para>For more information, see <see href="https://docs.cspr.cloud/rest-api/account/get-accounts">CSPR.Cloud API documentation</see>.</para>
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A paginated response containing account data.</returns>
            public Task<PaginatedResponse<AccountData>?> GetAccountsAsync(AccountsRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAccountsAsync(parameters, cancellationToken);
            }
            /// <summary>
            /// Get account info by account hash
            /// <para>For more information, see <see href="https://docs.cspr.cloud/rest-api/account-info/get-account-info">CSPR.Cloud API documentation</see>.</para>
            /// </summary>
            /// <param name="accountHash">The hash of the account.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A response containing account info data.</returns>
            public Task<Response<AccountInfoData>?> GetAccountInfoAsync(string accountHash, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAccountInfoAsync(accountHash, cancellationToken);
            }
            /// <summary>
            /// Get account infos
            /// <para>For more information, see <see href="https://docs.cspr.cloud/rest-api/account-info/get-account-infos">CSPR.Cloud API documentation</see>.</para>
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A paginated response containing account info data.</returns>
            public Task<PaginatedResponse<AccountInfoData>?> GetAccountInfosAsync(AccountInfosRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAccountInfosAsync(parameters, cancellationToken);
            }

        }
        /// <summary>
        /// Represents the Auction endpoints for interacting with the Casper Network auction data.
        /// </summary>
        public class Auction
        {
            private readonly CommonEndpoint _commonEndpoint;

            /// <summary>
            /// Initializes a new instance of the <see cref="Auction"/> class.
            /// </summary>
            /// <param name="commonEndpoint">The common endpoint to be used for auction operations.</param>
            public Auction(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            /// <summary>
            /// Retrieves the auction metrics asynchronously.
            /// <para>For more information, see <see href="https://docs.cspr.cloud/rest-api/auction-metrics/get-auction-metrics">CSPR.Cloud API documentation</see>.</para>
            /// </summary>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A singular response containing auction metrics data.</returns>
            public Task<Response<AuctionMetricsData>?> GetAuctionMetricsAsync(CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAuctionMetricsAsync(cancellationToken);
            }
        }

        /// <summary>
        /// Represents the Block endpoints for interacting with the Casper Network block data.
        /// </summary>
        public class Block
        {
            private readonly CommonEndpoint _commonEndpoint;

            /// <summary>
            /// Initializes a new instance of the <see cref="Block"/> class.
            /// </summary>
            /// <param name="commonEndpoint">The common endpoint to be used for block operations.</param>
            public Block(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            /// <summary>
            /// Retrieves a block asynchronously by its hash.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/block/get-block">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="blockHash">The hash of the block to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the block data.</returns>
            public Task<BlockData?> GetBlockAsync(string blockHash, BlockOptionalParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetBlockAsync(blockHash, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of blocks asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/block/get-blocks">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of block data.</returns>
            public Task<PaginatedResponse<BlockData>?> GetBlocksAsync(BlockRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetBlocksAsync(parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of blocks proposed by a specific validator asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/block/get-validator-blocks">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="validatorPublicKey">The public key of the validator whose blocks to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of block data.</returns>
            public Task<PaginatedResponse<BlockData>?> GetValidatorBlocksAsync(string validatorPublicKey, BlockRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetValidatorBlocksAsync(validatorPublicKey, parameters, cancellationToken);
            }
        }

        /// <summary>
        /// Represents the Bidder endpoints for interacting with the Casper Network bidder data.
        /// </summary>
        public class Bidder
        {
            private readonly CommonEndpoint _commonEndpoint;

            /// <summary>
            /// Initializes a new instance of the <see cref="Bidder"/> class.
            /// </summary>
            /// <param name="commonEndpoint">The common endpoint to be used for bidder operations.</param>
            public Bidder(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            /// <summary>
            /// Retrieves a bidder asynchronously by their public key.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/bidder/get-bidder">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the bidder to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the bidder data.</returns>
            public Task<BidderData?> GetBidderAsync(string publicKey, BidderRequestParameters parameters, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetBidderAsync(publicKey, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of bidders asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/bidder/get-bidders">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of bidder data.</returns>
            public Task<PaginatedResponse<BidderData>?> GetBiddersAsync(BiddersRequestParameters parameters, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetBiddersAsync(parameters, cancellationToken);
            }
        }

        /// <summary>
        /// Represents the CentralizedAccount endpoints for interacting with the Casper Network centralized account data.
        /// </summary>
        public class CentralizedAccount
        {
            private readonly CommonEndpoint _commonEndpoint;

            /// <summary>
            /// Initializes a new instance of the <see cref="CentralizedAccount"/> class.
            /// </summary>
            /// <param name="commonEndpoint">The common endpoint to be used for centralized account operations.</param>
            public CentralizedAccount(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            /// <summary>
            /// Retrieves centralized account information asynchronously by account hash.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/centralized-account-info/get-centralized-account-info">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="accountHash">The hash of the account to retrieve information for.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the centralized account information data.</returns>
            public Task<CentralizedAccountInfoData?> GetCentralizedAccountInfoAsync(string accountHash, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetCentralizedAccountInfoAsync(accountHash, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of centralized account information asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/centralized-account-info/get-centralized-account-infos">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of centralized account information data.</returns>
            public Task<PaginatedResponse<CentralizedAccountInfoData>?> GetCentralizedAccountInfosAsync(CentralizedAccountInfoRequestParameters parameters, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetCentralizedAccountInfosAsync(parameters, cancellationToken);
            }
        }

        /// <summary>
        /// Represents the Contract endpoints for interacting with the Casper Network contract data.
        /// </summary>
        public class Contract
        {
            private readonly CommonEndpoint _commonEndpoint;

            /// <summary>
            /// Initializes a new instance of the <see cref="Contract"/> class.
            /// </summary>
            /// <param name="commonEndpoint">The common endpoint to be used for contract operations.</param>
            public Contract(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            /// <summary>
            /// Retrieves a contract asynchronously by its hash.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract/get-contract">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractHash">The hash of the contract to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the contract data.</returns>
            public Task<ContractData?> GetContractAsync(string contractHash, ContractRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetContractAsync(contractHash, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of contracts asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract/get-contracts">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of contract data.</returns>
            public Task<PaginatedResponse<ContractData>?> GetContractsAsync(ContractsRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetContractsAsync(parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of contracts by contract package hash asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract/get-contracts-by-contract-package">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package to retrieve contracts for.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of contract data.</returns>
            public Task<PaginatedResponse<ContractData>?> GetContractsByContractPackageAsync(string contractPackageHash, ByContractRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetContractsByContractPackageAsync(contractPackageHash, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a list of contract types asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract/get-contract-types">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a list of contract type data.</returns>
            public Task<List<ContractTypeData>?> GetContractTypesAsync(CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetContractTypesAsync(cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of contract entry points asynchronously by contract hash.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract-entry-point/get-contract-entry-points">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractHash">The hash of the contract to retrieve entry points for.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of entry point data.</returns>
            public Task<PaginatedResponse<EntryPointData>?> GetContractEntryPointsAsync(string contractHash, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetContractEntryPointsAsync(contractHash, cancellationToken);
            }

            /// <summary>
            /// Retrieves the cost of a specific contract entry point asynchronously by contract hash and entry point name.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract-entry-point/get-contract-entry-point-costs">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractHash">The hash of the contract to retrieve entry point costs for.</param>
            /// <param name="entryPointName">The name of the entry point to retrieve costs for.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the entry point cost data.</returns>
            public Task<Response<EntryPointCostData>?> GetContractEntryPointCostsAsync(string contractHash, string entryPointName, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetContractEntryPointCostsAsync(contractHash, entryPointName, cancellationToken);
            }

            /// <summary>
            /// Retrieves a contract package asynchronously by its hash.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract-package/get-contract-package">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package to retrieve.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the contract package data.</returns>
            public Task<ContractResponse<ContractPackageData>?> GetContractPackageAsync(string contractPackageHash, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetContractPackageAsync(contractPackageHash, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of contract packages asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract-package/get-contract-packages">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of contract package data.</returns>
            public Task<PaginatedResponse<ContractPackageData>?> GetContractPackagesAsync(ContractPackageRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetContractPackagesAsync(parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of contract packages associated with a specific account asynchronously by public key.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract-package/get-account-contract-packages">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the account to retrieve contract packages for.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of contract package data.</returns>
            public Task<PaginatedResponse<ContractPackageData>?> GetAccountContractPackagesAsync(string publicKey, AccountContractPackageRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAccountContractPackagesAsync(publicKey, parameters, cancellationToken);
            }
        }

        /// <summary>
        /// Represents the Delegate endpoints for interacting with the Casper Network delegation data.
        /// </summary>
        public class Delegate
        {
            private readonly CommonEndpoint _commonEndpoint;

            /// <summary>
            /// Initializes a new instance of the <see cref="Delegate"/> class.
            /// </summary>
            /// <param name="commonEndpoint">The common endpoint to be used for delegation operations.</param>
            public Delegate(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            /// <summary>
            /// Retrieves a paginated list of delegations for a specific account asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/delegation/get-account-delegations">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the account whose delegations to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of delegation data.</returns>
            public Task<PaginatedResponse<DelegationData>?> GetAccountDelegationsAsync(string publicKey, DelegationRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAccountDelegationsAsync(publicKey, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of delegations for a specific validator asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/delegation/get-validator-delegations">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the validator whose delegations to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of delegation data.</returns>
            public Task<PaginatedResponse<DelegationData>?> GetValidatorDelegationsAsync(string publicKey, DelegationRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetValidatorDelegationsAsync(publicKey, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of delegator rewards for a specific account asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/delegator-reward/get-account-delegation-rewards">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the account whose delegator rewards to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of delegator reward data.</returns>
            public Task<PaginatedResponse<DelegatorRewardData>?> GetAccountDelegatorRewardsAsync(string publicKey, AccountDelegatorRewardRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAccountDelegatorRewardsAsync(publicKey, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves the total delegation rewards for a specific account asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/delegator-reward/get-account-total-delegation-rewards">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the account whose total delegation rewards to retrieve.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the total delegation rewards amount.</returns>
            public Task<ulong> GetTotalAccountDelegationRewards(string publicKey, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetTotalAccountDelegationRewards(publicKey, cancellationToken);
            }

            /// <summary>
            /// Retrieves the total delegation rewards for a specific validator asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/delegator-reward/get-total-validator-delegators-rewards">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the validator whose total delegation rewards to retrieve.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the total delegation rewards amount.</returns>
            public Task<ulong> GetTotalValidatorDelegationRewards(string publicKey, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetTotalValidatorDelegationRewards(publicKey, cancellationToken);
            }

            public Task<PaginatedResponse<DelegationData>?> GetPurseDelegationsAsync(string purseUref, DelegationRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetPurseDelegationsAsync(purseUref, parameters, cancellationToken);
            }

            public Task<PaginatedResponse<DelegatorRewardData>?> GetPurseDelegationRewardsAsync(string purseUref, AccountDelegatorRewardRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetPurseDelegationRewardsAsync(purseUref, parameters, cancellationToken);
            }

            public Task<ulong> GetTotalPurseDelegationRewardsAsync(string purseUref, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetTotalPurseDelegationRewardsAsync(purseUref, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of pending undelegations for a specific account asynchronously.
            /// Funds are released 7 eras after <see cref="UndelegationData.EraOfCreation"/>.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/delegation/get-account-undelegations">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the account whose pending undelegations to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of undelegation data.</returns>
            public Task<PaginatedResponse<UndelegationData>?> GetAccountUndelegationsAsync(string publicKey, DelegationRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAccountUndelegationsAsync(publicKey, parameters, cancellationToken);
            }
        }

        /// <summary>
        /// Represents the Deploy endpoints for interacting with the Casper Network deploy data.
        /// </summary>
        public class Deploy
        {
            private readonly CommonEndpoint _commonEndpoint;

            /// <summary>
            /// Initializes a new instance of the <see cref="Deploy"/> class.
            /// </summary>
            /// <param name="commonEndpoint">The common endpoint to be used for deploy operations.</param>
            public Deploy(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            /// <summary>
            /// Retrieves a deploy asynchronously by its hash.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/deploy/get-deploy">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="deployHash">The hash of the deploy to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the deploy data.</returns>
            public Task<Response<DeployData>?> GetDeployAsync(string deployHash, DeployRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetDeployAsync(deployHash, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of deploys asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/deploy/get-deploys">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of deploy data.</returns>
            public Task<PaginatedResponse<DeployData>?> GetDeploysAsync(DeploysRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetDeploysAsync(parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of deploys associated with a specific account asynchronously by public key.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/deploy/get-account-deploys">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the account whose deploys to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of deploy data.</returns>
            public Task<PaginatedResponse<DeployData>?> GetAccountDeploysAsync(string publicKey, AccountDeploysRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAccountDeploysAsync(publicKey, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of deploys included in a specific block asynchronously by block identifier.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/deploy/get-block-deploys">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="blockIdentifier">The identifier of the block whose deploys to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of deploy data.</returns>
            public Task<PaginatedResponse<DeployData>?> GetBlockDeploysAsync(string blockIdentifier, BlockDeploysRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetBlockDeploysAsync(blockIdentifier, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a list of deploy execution types asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/deploy/get-deploy-execution-types">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a list of deploy execution types data.</returns>
            public Task<Response<List<DeployExecutionTypesData>>?> GetDeployExecutionTypesAsync(CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetDeployExecutionTypesAsync(cancellationToken);
            }
        }

        /// <summary>
        /// Represents the FT endpoints for interacting with the Casper Network fungible token data.
        /// </summary>
        public class FT
        {
            private readonly CommonEndpoint _commonEndpoint;

            /// <summary>
            /// Initializes a new instance of the <see cref="FT"/> class.
            /// </summary>
            /// <param name="commonEndpoint">The common endpoint to be used for fungible token operations.</param>
            public FT(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            /// <summary>
            /// Retrieves a paginated list of fungible token actions asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/fungible-token-action/get-token-actions">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of fungible token action data.</returns>
            public Task<PaginatedResponse<FTTokenActionData>?> GetFTActionsAsync(FTActionRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetFungibleTokenActionsAsync(parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of fungible token actions for a specific account asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/fungible-token-action/get-account-token-actions">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="accountIdentifier">The identifier of the account whose fungible token actions to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of fungible token action data.</returns>
            public Task<PaginatedResponse<FTTokenActionData>?> GetAccountFTActionsAsync(string accountIdentifier, FTAccountActionRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAccountFungibleTokenActionsAsync(accountIdentifier, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of fungible token actions for a specific contract package asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/fungible-token-action/get-contract-package-token-actions">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package whose fungible token actions to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of fungible token action data.</returns>
            public Task<PaginatedResponse<FTTokenActionData>?> GetContractPackageFTActionsAsync(string contractPackageHash, FTContractPackageActionRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetContractPackageFungibleTokenActionsAsync(contractPackageHash, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of fungible token ownership data for a specific account asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/fungible-token-ownership/get-account-fungible-token-ownership">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="accountIdentifier">The identifier of the account whose fungible token ownership data to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of fungible token ownership data.</returns>
            public Task<PaginatedResponse<FTOwnershipData>?> GetAccountFTOwnershipAsync(string accountIdentifier, FTAccountOwnershipRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAccountFungibleTokenOwnershipAsync(accountIdentifier, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of fungible token ownership data for a specific contract package asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/fungible-token-ownership/get-contract-package-fungible-token-ownership">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package whose fungible token ownership data to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of fungible token ownership data.</returns>
            public Task<PaginatedResponse<FTOwnershipData>?> GetContractPackageFTOwnershipAsync(string contractPackageHash, FTContractPackageOwnershipRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetContractPackageFungibleTokenOwnershipAsync(contractPackageHash, parameters, cancellationToken);
            }

            public Task<ListResponse<FTActionTypeData>?> GetFTTokenActionTypesAsync(CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetFTTokenActionTypesAsync(cancellationToken);
            }

            public Task<FTRateData?> GetFTRateLatestAsync(string contractPackageHash, FTRateFilterParameters filterParameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetFTRateLatestAsync(contractPackageHash, filterParameters, cancellationToken);
            }

            public Task<PaginatedResponse<FTRateData>?> GetFTRatesAsync(string contractPackageHash, FTRateRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetFTRatesAsync(contractPackageHash, parameters, cancellationToken);
            }

            public Task<FTDailyRateData?> GetFTDailyRateLatestAsync(string contractPackageHash, FTRateFilterParameters filterParameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetFTDailyRateLatestAsync(contractPackageHash, filterParameters, cancellationToken);
            }

            public Task<PaginatedResponse<FTDailyRateData>?> GetFTDailyRatesAsync(string contractPackageHash, FTDailyRateRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetFTDailyRatesAsync(contractPackageHash, parameters, cancellationToken);
            }

            public Task<FTDexRateData?> GetFTDexRateLatestAsync(string contractPackageHash, FTDexRateFilterParameters filterParameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetFTDexRateLatestAsync(contractPackageHash, filterParameters, cancellationToken);
            }

            public Task<PaginatedResponse<FTDexRateData>?> GetFTDexRatesAsync(string contractPackageHash, FTDexRateRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetFTDexRatesAsync(contractPackageHash, parameters, cancellationToken);
            }

            public Task<FTDailyDexRateData?> GetFTDailyDexRateLatestAsync(string contractPackageHash, FTDexRateFilterParameters filterParameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetFTDailyDexRateLatestAsync(contractPackageHash, filterParameters, cancellationToken);
            }

            public Task<PaginatedResponse<FTDailyDexRateData>?> GetFTDailyDexRatesAsync(string contractPackageHash, FTDailyDexRateRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetFTDailyDexRatesAsync(contractPackageHash, parameters, cancellationToken);
            }
        }

        /// <summary>
        /// Represents the NFT endpoints for interacting with the Casper Network non-fungible token data.
        /// </summary>
        public class NFT
        {
            private readonly CommonEndpoint _commonEndpoint;

            /// <summary>
            /// Initializes a new instance of the <see cref="NFT"/> class.
            /// </summary>
            /// <param name="commonEndpoint">The common endpoint to be used for non-fungible token operations.</param>
            public NFT(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            /// <summary>
            /// Retrieves an NFT token asynchronously by its contract package hash and token ID.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token/get-contract-package-token-by-token-id">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package.</param>
            /// <param name="tokenId">The ID of the token to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the NFT token data.</returns>
            public Task<Response<NFTTokenData>?> GetNFTAsync(string contractPackageHash, string tokenId, NFTRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetNFTAsync(contractPackageHash, tokenId, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of NFTs for a specific account asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token/get-account-tokens">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="accountIdentifier">The identifier of the account whose NFTs to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of NFT token data.</returns>
            public Task<PaginatedResponse<NFTTokenData>?> GetAccountNFTsAsync(string accountIdentifier, NFTAccountRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAccountNFTsAsync(accountIdentifier, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of NFTs for a specific contract package asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token/get-contract-package-tokens">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package whose NFTs to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of NFT token data.</returns>
            public Task<PaginatedResponse<NFTTokenData>?> GetContractPackageNFTsAsync(string contractPackageHash, NFTContractPackageRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetContractPackageNFTsAsync(contractPackageHash, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a list of NFT standards asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token/get-token-standards">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a list response of NFT standard data.</returns>
            public Task<ListResponse<NFTStandardData>?> GetNFTStandardsAsync(CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetNFTStandardsAsync(cancellationToken);
            }

            /// <summary>
            /// Retrieves a list of offchain NFT metadata statuses asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token/get-token-offchain-metadata-statuses">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a list response of NFT metadata status data.</returns>
            public Task<ListResponse<NFTMetadataStatusData>?> GetOffchainNFTMetadataStatusesAsync(CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetOffchainNFTMetadataStatusesAsync(cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of NFT actions for a specific contract package and token asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token-action/get-contract-package-token-actions-by-token">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package.</param>
            /// <param name="tokenId">The ID of the token whose actions to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of NFT token action data.</returns>
            public Task<PaginatedResponse<NFTTokenActionData>?> GetContractPackageNFTActionsForATokenAsync(string contractPackageHash, string tokenId, NFTContractPackageTokenActionsRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetContractPackageNFTActionsForATokenAsync(contractPackageHash, tokenId, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of NFT actions for a specific account asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token-action/get-account-token-actions">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="accountIdentifier">The identifier of the account whose NFT actions to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of NFT token action data.</returns>
            public Task<PaginatedResponse<NFTTokenActionData>?> GetAccountNFTActionsAsync(string accountIdentifier, NFTAccountActionsRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAccountNFTActionsAsync(accountIdentifier, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of NFT actions for a specific contract package asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token-action/get-contract-package-token-actions">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package whose NFT actions to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of NFT token action data.</returns>
            public Task<PaginatedResponse<NFTTokenActionData>?> GetContractPackageNFTActionsAsync(string contractPackageHash, NFTContractPackageActionsRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetContractPackageNFTActionsAsync(contractPackageHash, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a list of NFT action types asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token-action/get-token-actions-types">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a list response of NFT action types data.</returns>
            public Task<ListResponse<NFTActionTypesData>?> GetNFTActionTypesAsync(CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetNFTActionTypesAsync(cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of NFT ownership data for a specific contract package asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token-ownership/get-contract-package-token-ownership">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package whose NFT ownership data to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of NFT token ownership data.</returns>
            public Task<PaginatedResponse<NFTTokenOwnershipData>?> GetContractPackageNFTOwnershipAsync(string contractPackageHash, NFTContractPackageOwnershipRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetContractPackageNFTOwnershipAsync(contractPackageHash, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of NFT ownership data for a specific account asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token-ownership/get-account-token-ownership">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="accountIdentifier">The identifier of the account whose NFT ownership data to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of NFT token ownership data.</returns>
            public Task<PaginatedResponse<NFTTokenOwnershipData>?> GetAccountNFTOwnershipAsync(string accountIdentifier, NFTAccountOwnershipRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAccountNFTOwnershipAsync(accountIdentifier, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated, network-wide list of NFTs asynchronously, optionally filtered by contract package, owner, or block-height range.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token/get-tokens">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request (filters, includers, sort, pagination).</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of NFT token data.</returns>
            public Task<PaginatedResponse<NFTTokenData>?> GetNFTsAsync(NFTsRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetNFTsAsync(parameters, cancellationToken);
            }
        }

        /// <summary>
        /// Represents the rate endpoints for interacting with currency and rate data on the Casper Network.
        /// </summary>
        public class Rate
        {
            private readonly CommonEndpoint _commonEndpoint;

            /// <summary>
            /// Initializes a new instance of the <see cref="Rate"/> class.
            /// </summary>
            /// <param name="commonEndpoint">The common endpoint to be used for rate operations.</param>
            public Rate(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            /// <summary>
            /// Retrieves the current currency rate asynchronously for a specified currency ID.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/cspr-rate/get-current-currency-rate">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="currencyId">The ID of the currency for which to retrieve the rate.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the current rate data.</returns>
            public Task<Response<RateData>?> GetCurrentCurrencyRateAsync(string currencyId, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetCurrentCurrencyRateAsync(currencyId, cancellationToken);
            }

            /// <summary>
            /// Retrieves historical currency rates asynchronously for a specified currency ID.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/cspr-rate/get-historical-currency-rates">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="currencyId">The ID of the currency for which to retrieve historical rates.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of historical rate data.</returns>
            public Task<PaginatedResponse<RateData>?> GetHistoricalCurrencyRatesAsync(string currencyId, RateHistoricalRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetHistoricalCurrencyRatesAsync(currencyId, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves a paginated list of currencies asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/cspr-rate/get-currencies">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of currency data.</returns>
            public Task<PaginatedResponse<CurrencyData>?> GetCurrenciesAsync(RateCurrenciesRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetCurrenciesAsync(parameters, cancellationToken);
            }
        }

        /// <summary>
        /// Represents the supply endpoint for retrieving supply data on the Casper Network.
        /// </summary>
        public class Supply
        {
            private readonly CommonEndpoint _commonEndpoint;

            /// <summary>
            /// Initializes a new instance of the <see cref="Supply"/> class.
            /// </summary>
            /// <param name="commonEndpoint">The common endpoint to be used for supply operations.</param>
            public Supply(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            /// <summary>
            /// Retrieves the current supply data asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/cspr-supply/get-supply">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the current supply data.</returns>
            public Task<Response<SupplyData>?> GetSupplyAsync(CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetSupplyAsync(cancellationToken);
            }
        }


        /// <summary>
        /// Represents the transfer endpoint for retrieving transfer data on the Casper Network.
        /// </summary>
        public class Transfer
        {
            private readonly CommonEndpoint _commonEndpoint;

            /// <summary>
            /// Initializes a new instance of the <see cref="Transfer"/> class.
            /// </summary>
            /// <param name="commonEndpoint">The common endpoint to be used for transfer operations.</param>
            public Transfer(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            /// <summary>
            /// Retrieves account transfers asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/transfer/get-account-transfers">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="accountIdentifier">The identifier of the account for which transfers are to be retrieved.</param>
            /// <param name="parameters">Optional parameters to filter or paginate results.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains paginated transfer data for the account.</returns>
            public Task<PaginatedResponse<TransferData>?> GetAccountTransfersAsync(string accountIdentifier, TransferAccountRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAccountTransfersAsync(accountIdentifier, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves deploy transfers asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/transfer/get-deploy-transfers">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="deployHash">The hash of the deploy for which transfers are to be retrieved.</param>
            /// <param name="parameters">Optional parameters to filter or paginate results.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains paginated transfer data for the deploy.</returns>
            public Task<PaginatedResponse<TransferData>?> GetDeployTransfersAsync(string deployHash, TransferDeployRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetDeployTransfersAsync(deployHash, parameters, cancellationToken);
            }

            public Task<PaginatedResponse<TransferData>?> GetPurseTransfersAsync(string purseUref, TransferAccountRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetPurseTransfersAsync(purseUref, parameters, cancellationToken);
            }
        }

        /// <summary>
        /// Represents the validator endpoint for retrieving validator data on the Casper Network.
        /// </summary>
        public class Validator
        {
            private readonly CommonEndpoint _commonEndpoint;

            /// <summary>
            /// Initializes a new instance of the <see cref="Validator"/> class.
            /// </summary>
            /// <param name="commonEndpoint">The common endpoint to be used for validator operations.</param>
            public Validator(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            /// <summary>
            /// Retrieves validator details asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator/get-validator">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the validator for which details are to be retrieved.</param>
            /// <param name="parameters">Optional parameters to filter or paginate results.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains details of the validator.</returns>
            public Task<Response<ValidatorData>?> GetValidatorAsync(string publicKey, ValidatorRequestParameters parameters, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetValidatorAsync(publicKey, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves validators asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator/get-validators">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters to filter or paginate results.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a list of validators.</returns>
            public Task<PaginatedResponse<ValidatorData>?> GetValidatorsAsync(ValidatorsRequestParameters parameters, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetValidatorsAsync(parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves historical performance of a validator asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator-performance/get-historical-validator-performance">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the validator for which historical performance is to be retrieved.</param>
            /// <param name="parameters">Optional parameters to filter or paginate results.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains historical performance data of the validator.</returns>
            public Task<PaginatedResponse<RelativeValidatorPerformanceData>?> GetHistoricalValidatorPerformanceAsync(string publicKey, ValidatorHistoricalPerformanceRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetHistoricalValidatorPerformanceAsync(publicKey, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves historical average performance of a validator asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator-performance/get-historical-average-validator-performance">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the validator for which historical average performance is to be retrieved.</param>
            /// <param name="parameters">Optional parameters to filter or paginate results.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains historical average performance data of the validator.</returns>
            public Task<PaginatedResponse<ValidatorPerformanceData>?> GetHistoricalValidatorAveragePerformanceAsync(string publicKey, ValidatorHistoricalAveragePerformanceRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetHistoricalValidatorAveragePerformanceAsync(publicKey, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves historical average performance of validators asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator-performance/get-historical-average-validators-performance">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters to filter or paginate results.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains historical average performance data of validators.</returns>
            public Task<PaginatedResponse<RelativeValidatorPerformanceData>?> GetHistoricalValidatorsAveragePerformanceAsync(ValidatorsHistoricalAveragePerformanceRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetHistoricalValidatorsAveragePerformanceAsync(parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves validator rewards asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator-reward/get-validator-rewards">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the validator for which rewards are to be retrieved.</param>
            /// <param name="parameters">Optional parameters to filter or paginate results.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains validator rewards data.</returns>
            public Task<PaginatedResponse<ValidatorRewardData>?> GetValidatorRewardsAsync(string publicKey, ValidatorRewardsRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetValidatorRewardsAsync(publicKey, parameters, cancellationToken);
            }

            /// <summary>
            /// Retrieves total rewards earned by a validator asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator-reward/get-validator-total-rewards">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the validator for which total rewards are to be retrieved.</param>
            /// <param name="cancellationToken">A token to cancel the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the total rewards earned by the validator.</returns>
            public Task<Response<ulong>?> GetValidatorTotalRewardsAsync(string publicKey, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetValidatorTotalRewardsAsync(publicKey, cancellationToken);
            }

            public Task<PaginatedResponse<ValidatorRewardData>?> GetValidatorEraRewardsAsync(string publicKey, ValidatorEraRewardsRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetValidatorEraRewardsAsync(publicKey, parameters, cancellationToken);
            }
        }

        // Add to FT wrapper - FT Rate + Action Type methods
        // (handled below via new wrapper classes)

        public class DexEndpoint
        {
            private readonly CommonEndpoint _commonEndpoint;

            public DexEndpoint(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            public Task<ListResponse<DexData>?> GetDexesAsync(CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetDexesAsync(cancellationToken);
            }
        }

        public class CsprNameEndpoint
        {
            private readonly CommonEndpoint _commonEndpoint;

            public CsprNameEndpoint(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            public Task<CsprNameResolutionData?> GetCsprNameResolutionAsync(string name, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetCsprNameResolutionAsync(name, cancellationToken);
            }
        }

        public class SwapEndpoint
        {
            private readonly CommonEndpoint _commonEndpoint;

            public SwapEndpoint(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            public Task<PaginatedResponse<SwapData>?> GetSwapsAsync(SwapRequestParameters parameters = null, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetSwapsAsync(parameters, cancellationToken);
            }
        }

        public class AwaitingDeployEndpoint
        {
            private readonly CommonEndpoint _commonEndpoint;

            public AwaitingDeployEndpoint(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }

            public Task<Response<bool>?> CreateAwaitingDeployAsync(CreateAwaitingDeployRequest request, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.CreateAwaitingDeployAsync(request, cancellationToken);
            }

            public Task<Response<bool>?> AddAwaitingDeployApprovalsAsync(string deployHash, AddApprovalRequest request, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.AddAwaitingDeployApprovalsAsync(deployHash, request, cancellationToken);
            }

            public Task<AwaitingDeployData?> GetAwaitingDeployAsync(string deployHash, CancellationToken cancellationToken = default)
            {
                return _commonEndpoint.GetAwaitingDeployAsync(deployHash, cancellationToken);
            }
        }


    }
}
