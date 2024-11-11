using CSPR.Cloud.Net.Clients.Api;
using CSPR.Cloud.Net.Errors;
using CSPR.Cloud.Net.Objects.Abstract;
using CSPR.Cloud.Net.Objects.Account;
using CSPR.Cloud.Net.Objects.AccountInfo;
using CSPR.Cloud.Net.Objects.Auction;
using CSPR.Cloud.Net.Objects.Bidder;
using CSPR.Cloud.Net.Objects.Block;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using CSPR.Cloud.Net.Objects.Config;
using CSPR.Cloud.Net.Objects.Contract;
using CSPR.Cloud.Net.Objects.Delegate;
using CSPR.Cloud.Net.Objects.Deploy;
using CSPR.Cloud.Net.Objects.Ft;
using CSPR.Cloud.Net.Objects.Nft;
using CSPR.Cloud.Net.Objects.Rate;
using CSPR.Cloud.Net.Objects.Supply;
using CSPR.Cloud.Net.Objects.Transfer;
using CSPR.Cloud.Net.Objects.Validator;
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
using CSPR.Cloud.Net.Parameters.Wrapper.Transfer;
using CSPR.Cloud.Net.Parameters.Wrapper.Validator;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSPR.Cloud.Net.Clients
{
    public class CasperCloudRestClient
    {
        private readonly string _apiKey;

        private readonly HttpClient _httpClient;

        private readonly ILogger? _logger;

        public MainnetEndpoint Mainnet { get; }
        public TestnetEndpoint Testnet { get; }
        // Primary constructor
        public CasperCloudRestClient(CasperCloudClientConfig config, HttpClient? httpClient = null, ILoggerFactory? loggerFactory = null)
        {
            _apiKey = config.ApiKey;
            _httpClient = httpClient ?? new HttpClient();
            _logger = loggerFactory?.CreateLogger<CasperCloudRestClient>();
            Mainnet = new MainnetEndpoint(this);
            Testnet = new TestnetEndpoint(this);
        }


        public async Task<T> GetDataAsync<T>(string endpoint) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{endpoint}");
            request.Headers.Add("Authorization", _apiKey);
            request.Headers.Add("Accept", "application/json");

            var response = await _httpClient.SendAsync(request);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var content = await response.Content.ReadAsStringAsync();
                    T result = JsonConvert.DeserializeObject<T>(content);
                    return result ?? throw new Exception("Failed to deserialize response content.");

                case HttpStatusCode.BadRequest:
                    throw new InvalidParamException($"Invalid Param Error: {await response.Content.ReadAsStringAsync()}", _logger);

                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException($"Unauthorized Error: {await response.Content.ReadAsStringAsync()}", _logger);

                case HttpStatusCode.Forbidden:
                    throw new AccessDeniedException($"Access Denied Error: {await response.Content.ReadAsStringAsync()}", _logger);

                case HttpStatusCode.NotFound:
                    throw new NotFoundException($"Not Found Error: {await response.Content.ReadAsStringAsync()}", _logger);

                case HttpStatusCode.Conflict:
                    throw new DuplicateEntryException($"Duplicate Entry Error: {await response.Content.ReadAsStringAsync()}", _logger);

                case HttpStatusCode.InternalServerError:
                    throw new InternalServerErrorException($"Internal Server Error: {await response.Content.ReadAsStringAsync()}", _logger);

                default:
                    throw new HttpRequestException($"Error: {response.StatusCode}");
            }
        }

        public class MainnetEndpoint
        {
            private readonly CommonEndpoint _commonEndpoint;
            public Account Account { get; }
            public Auction Auction { get; }
            public Block Block { get; }
            public Bidder Bidder { get; }
            public CentralizedAccount CentralizedAccount { get; }
            public Contract Contract { get; }
            public Delegate Delegate { get; }
            public Deploy Deploy { get; }
            public FT FT { get; }
            public NFT NFT { get; }
            public Rate Rate { get; }
            public Supply Supply { get; }
            public Transfer Transfer { get; }
            public Validator Validator { get; }
            public MainnetEndpoint(CasperCloudRestClient casperCloudRestClient)
            {
                _commonEndpoint = new CommonEndpoint(casperCloudRestClient, Endpoints.BaseUrls.Mainnet);
                Account = new Account(_commonEndpoint);
                Auction = new Auction(_commonEndpoint);
                Block = new Block(_commonEndpoint);
                Bidder = new Bidder(_commonEndpoint);
                CentralizedAccount = new CentralizedAccount(_commonEndpoint);
                Contract = new Contract(_commonEndpoint);
                Delegate = new Delegate(_commonEndpoint);
                Deploy = new Deploy(_commonEndpoint);
                FT = new FT(_commonEndpoint);
                NFT = new NFT(_commonEndpoint);
                Rate = new Rate(_commonEndpoint);
                Supply = new Supply(_commonEndpoint);
                Transfer = new Transfer(_commonEndpoint);
                Validator = new Validator(_commonEndpoint);
            }

        }
        public class TestnetEndpoint
        {
            private readonly CommonEndpoint _commonEndpoint;
            public Account Account { get; }
            public Auction Auction { get; }
            public Block Block { get; }
            public Bidder Bidder { get; }
            public CentralizedAccount CentralizedAccount { get; }
            public Contract Contract { get; }
            public Delegate Delegate { get; }
            public Deploy Deploy { get; }
            public FT FT { get; }
            public NFT NFT { get; }
            public Rate Rate { get; }
            public Supply Supply { get; }
            public Transfer Transfer { get; }
            public Validator Validator { get; }
            public TestnetEndpoint(CasperCloudRestClient casperCloudRestClient)
            {
                _commonEndpoint = new CommonEndpoint(casperCloudRestClient, Endpoints.BaseUrls.Testnet);
                Account = new Account(_commonEndpoint);
                Block = new Block(_commonEndpoint);
                Bidder = new Bidder(_commonEndpoint);
                CentralizedAccount = new CentralizedAccount(_commonEndpoint);
                Contract = new Contract(_commonEndpoint);
                Delegate = new Delegate(_commonEndpoint);
                Deploy = new Deploy(_commonEndpoint);
                FT = new FT(_commonEndpoint);
                NFT = new NFT(_commonEndpoint);
                Rate = new Rate(_commonEndpoint);
                Supply = new Supply(_commonEndpoint);
                Transfer = new Transfer(_commonEndpoint);
                Validator = new Validator(_commonEndpoint);
                Auction = new Auction(_commonEndpoint);

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

            public async Task<AccountData> GetAccountAsync(string publicKey, AccountsOptionalParameters parameters = null)
            {
                string endpoint = Endpoints.Account.GetAccount(_baseUrl, publicKey, parameters);
                var response = await _casperCloudRestClient.GetDataAsync<Response<AccountData>>(endpoint);
                return response.Data;
            }
            public async Task<PaginatedResponse<AccountData>> GetAccountsAsync(AccountsRequestParameters parameters)
            {

                string endpoint = Endpoints.Account.GetAccounts(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<AccountData>>(endpoint);
            }
            public async Task<BlockData> GetBlockAsync(string blockHash, BlockOptionalParameters parameters = null)
            {
                string endpoint = Endpoints.Block.GetBlock(_baseUrl, blockHash, parameters);
                var response = await _casperCloudRestClient.GetDataAsync<Response<BlockData>>(endpoint);
                return response.Data;
            }
            public async Task<PaginatedResponse<BlockData>> GetBlocksAsync(BlockRequestParameters parameters)
            {
                string endpoint = Endpoints.Block.GetBlocks(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<BlockData>>(endpoint);
            }
            public async Task<PaginatedResponse<BlockData>> GetValidatorBlocksAsync(string validatorPublicKey, BlockRequestParameters parameters)
            {
                string endpoint = Endpoints.Block.GetValidatorBlocks(_baseUrl, validatorPublicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<BlockData>>(endpoint);
            }
            public async Task<BidderData> GetBidderAsync(string publicKey, BidderRequestParameters parameters)
            {
                string endpoint = Endpoints.Bidder.GetBidder(_baseUrl, publicKey, parameters);
                var response = await _casperCloudRestClient.GetDataAsync<Response<BidderData>>(endpoint);
                return response.Data;
            }
            public async Task<PaginatedResponse<BidderData>> GetBiddersAsync(BiddersRequestParameters parameters)
            {
                string endpoint = Endpoints.Bidder.GetBidders(_baseUrl, parameters);
                var response = await _casperCloudRestClient.GetDataAsync<PaginatedResponse<BidderData>>(endpoint);
                return response;
            }
            public async Task<CentralizedAccountInfoData> GetCentralizedAccountInfoAsync(string accountHash)
            {
                string endpoint = Endpoints.CentralizedAccountInfo.GetCentralizedAccountInfo(_baseUrl, accountHash);
                var response = await _casperCloudRestClient.GetDataAsync<Response<CentralizedAccountInfoData>>(endpoint);
                return response.Data;
            }
            public async Task<PaginatedResponse<CentralizedAccountInfoData>> GetCentralizedAccountInfosAsync(CentralizedAccountInfoRequestParameters parameters)
            {
                string endpoint = Endpoints.CentralizedAccountInfo.GetCentralizedInfos(_baseUrl, parameters);
                var response = await _casperCloudRestClient.GetDataAsync<PaginatedResponse<CentralizedAccountInfoData>>(endpoint);
                return response;
            }
            public async Task<ContractData> GetContractAsync(string contractHash, ContractRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Contract.GetContract(_baseUrl, contractHash, parameters);
                var response = await _casperCloudRestClient.GetDataAsync<Response<ContractData>>(endpoint);
                return response.Data;
            }
            public async Task<PaginatedResponse<ContractData>> GetContractsAsync(ContractsRequestParameters parameters)
            {
                string endpoint = Endpoints.Contract.GetContracts(_baseUrl, parameters);
                var response = await _casperCloudRestClient.GetDataAsync<PaginatedResponse<ContractData>>(endpoint);
                return response;
            }
            public async Task<PaginatedResponse<ContractData>> GetContractsByContractPackageAsync(string contractPackageHash, ByContractRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Contract.GetContractsByContractPackage(_baseUrl, contractPackageHash, parameters);
                var response = await _casperCloudRestClient.GetDataAsync<PaginatedResponse<ContractData>>(endpoint);
                return response;
            }
            public async Task<List<ContractTypeData>> GetContractTypesAsync()
            {
                string endpoint = Endpoints.Contract.GetContractTypes(_baseUrl);
                var response = await _casperCloudRestClient.GetDataAsync<Response<List<ContractTypeData>>>(endpoint);
                return response.Data;
            }
            public async Task<PaginatedResponse<EntryPointData>> GetContractEntryPointsAsync(string contractHash)
            {
                string endpoint = Endpoints.Contract.GetContractEntryPoints(_baseUrl, contractHash);
                var response = await _casperCloudRestClient.GetDataAsync<PaginatedResponse<EntryPointData>>(endpoint);
                return response;
            }
            public async Task<Response<EntryPointCostData>> GetContractEntryPointCostsAsync(string contractHash, string entryPointName)
            {
                string endpoint = Endpoints.Contract.GetContractEntryPointCosts(_baseUrl, contractHash, entryPointName);
                return await _casperCloudRestClient.GetDataAsync<Response<EntryPointCostData>>(endpoint);
            }
            public async Task<ContractResponse<ContractPackageData>> GetContractPackageAsync(string contractPackageHash)
            {
                string endpoint = Endpoints.Contract.GetContractPackage(_baseUrl, contractPackageHash);
                return await _casperCloudRestClient.GetDataAsync<ContractResponse<ContractPackageData>>(endpoint);
            }
            public async Task<PaginatedResponse<ContractPackageData>> GetContractPackagesAsync(ContractPackageRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Contract.GetContractPackages(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<ContractPackageData>>(endpoint);
            }
            public async Task<PaginatedResponse<ContractPackageData>> GetAccountContractPackagesAsync(string publicKey, AccountContractPackageRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Contract.GetAccountContractPackages(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<ContractPackageData>>(endpoint);
            }
            public async Task<PaginatedResponse<DelegationData>> GetAccountDelegationsAsync(string publicKey, DelegationRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Delegate.GetAccountDelegations(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<DelegationData>>(endpoint);
            }
            public async Task<PaginatedResponse<DelegationData>> GetValidatorDelegationsAsync(string publicKey, DelegationRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Delegate.GetValidatorDelegations(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<DelegationData>>(endpoint);
            }
            public async Task<PaginatedResponse<DelegatorRewardData>> GetAccountDelegatorRewardsAsync(string publicKey, AccountDelegatorRewardRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Delegate.GetAccountDelegatorRewards(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<DelegatorRewardData>>(endpoint);
            }
            public async Task<ulong> GetTotalAccountDelegationRewards(string publicKey)
            {
                string endpoint = Endpoints.Delegate.GetTotalAccountDelegationRewards(_baseUrl, publicKey);
                var response = await _casperCloudRestClient.GetDataAsync<Response<ulong>>(endpoint);
                return response.Data;
            }
            public async Task<ulong> GetTotalValidatorDelegationRewards(string publicKey)
            {
                string endpoint = Endpoints.Delegate.GetTotalValidatorDelegatorsRewards(_baseUrl, publicKey);
                var response = await _casperCloudRestClient.GetDataAsync<Response<ulong>>(endpoint);
                return response.Data;
            }
            public async Task<Response<DeployData>> GetDeployAsync(string deployHash, DeployRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Deploy.GetDeploy(_baseUrl, deployHash, parameters);
                var response = await _casperCloudRestClient.GetDataAsync<Response<DeployData>>(endpoint);
                return response;
            }
            public async Task<PaginatedResponse<DeployData>> GetDeploysAsync(DeploysRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Deploy.GetDeploys(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<DeployData>>(endpoint);
            }
            public async Task<PaginatedResponse<DeployData>> GetAccountDeploysAsync(string publicKey, AccountDeploysRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Deploy.GetAccountDeploys(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<DeployData>>(endpoint);
            }
            public async Task<PaginatedResponse<DeployData>> GetBlockDeploysAsync(string blockIdentifier, BlockDeploysRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Deploy.GetBlockDeploys(_baseUrl, blockIdentifier, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<DeployData>>(endpoint);
            }
            public async Task<Response<List<DeployExecutionTypesData>>> GetDeployExecutionTypesAsync()
            {
                string endpoint = Endpoints.Deploy.GetDeployExecutionTypes(_baseUrl);
                return await _casperCloudRestClient.GetDataAsync<Response<List<DeployExecutionTypesData>>>(endpoint);
            }
            public async Task<PaginatedResponse<FTTokenActionData>> GetFungibleTokenActionsAsync(FTActionRequestParameters parameters = null)
            {
                string endpoint = Endpoints.FT.GetFungibleTokenActions(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<FTTokenActionData>>(endpoint);

            }
            public async Task<PaginatedResponse<FTTokenActionData>> GetAccountFungibleTokenActionsAsync(string accountIdentifier, FTAccountActionRequestParameters parameters = null)
            {
                string endpoint = Endpoints.FT.GetAccountFungibleTokenActions(_baseUrl, accountIdentifier, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<FTTokenActionData>>(endpoint);

            }
            public async Task<PaginatedResponse<FTTokenActionData>> GetContractPackageFungibleTokenActionsAsync(string contractPackageHash, FTContractPackageActionRequestParameters parameters = null)
            {
                string endpoint = Endpoints.FT.GetContractPackageFungibleTokenActions(_baseUrl, contractPackageHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<FTTokenActionData>>(endpoint);

            }
            public async Task<PaginatedResponse<FTOwnershipData>> GetAccountFungibleTokenOwnershipAsync(string accountIdentifier, FTAccountOwnershipRequestParameters parameters = null)
            {
                string endpoint = Endpoints.FT.GetAccountFungibleTokenOwnership(_baseUrl, accountIdentifier, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<FTOwnershipData>>(endpoint);
            }
            public async Task<PaginatedResponse<FTOwnershipData>> GetContractPackageFungibleTokenOwnershipAsync(string contractPackageHash)
            {
                string endpoint = Endpoints.FT.GetContractPackageFungibleTokenOwnership(_baseUrl, contractPackageHash);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<FTOwnershipData>>(endpoint);
            }
            public async Task<Response<NFTTokenData>> GetNFTAsync(string contractPackageHash, string tokenId, NFTRequestParameters parameters = null)
            {
                string endpoint = Endpoints.NFT.GetNonFungibleToken(_baseUrl, contractPackageHash, tokenId, parameters);
                return await _casperCloudRestClient.GetDataAsync<Response<NFTTokenData>>(endpoint);
            }
            public async Task<PaginatedResponse<NFTTokenData>> GetAccountNFTsAsync(string accountIdentifier, NFTAccountRequestParameters parameters = null)
            {
                string endpoint = Endpoints.NFT.GetAccountNFTs(_baseUrl, accountIdentifier, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<NFTTokenData>>(endpoint);
            }
            public async Task<PaginatedResponse<NFTTokenData>> GetContractPackageNFTsAsync(string contractPackageHash, NFTContractPackageRequestParameters parameters = null)
            {
                string endpoint = Endpoints.NFT.GetContractPackageNFTs(_baseUrl, contractPackageHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<NFTTokenData>>(endpoint);
            }
            public async Task<ListResponse<NFTStandardData>> GetNFTStandardsAsync()
            {
                string endpoint = Endpoints.NFT.GetNFTStandards(_baseUrl);
                return await _casperCloudRestClient.GetDataAsync<ListResponse<NFTStandardData>>(endpoint);
            }
            public async Task<ListResponse<NFTMetadataStatusData>> GetOffchainNFTMetadataStatusesAsync()
            {
                string endpoint = Endpoints.NFT.GetOffchainNFTMetadataStatuses(_baseUrl);
                return await _casperCloudRestClient.GetDataAsync<ListResponse<NFTMetadataStatusData>>(endpoint);
            }
            public async Task<PaginatedResponse<NFTTokenActionData>> GetContractPackageNFTActionsForATokenAsync(string contractPackageHash, string tokenId, NFTContractPackageTokenActionsRequestParameters parameters = null)
            {
                string endpoint = Endpoints.NFT.GetContractPackageActionsForAToken(_baseUrl, contractPackageHash, tokenId, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<NFTTokenActionData>>(endpoint);
            }
            public async Task<PaginatedResponse<NFTTokenActionData>> GetAccountNFTActionsAsync(string accountIdentifier, NFTAccountActionsRequestParameters parameters = null)
            {
                string endpoint = Endpoints.NFT.GetAccountNFTActions(_baseUrl, accountIdentifier, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<NFTTokenActionData>>(endpoint);
            }
            public async Task<PaginatedResponse<NFTTokenActionData>> GetContractPackageNFTActionsAsync(string contractPackageHash, NFTContractPackageActionsRequestParameters parameters = null)
            {
                string endpoint = Endpoints.NFT.GetContractPackageNFTActions(_baseUrl, contractPackageHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<NFTTokenActionData>>(endpoint);
            }
            public async Task<ListResponse<NFTActionTypesData>> GetNFTActionTypesAsync()
            {
                string endpoint = Endpoints.NFT.GetNFTActionTypes(_baseUrl);
                return await _casperCloudRestClient.GetDataAsync<ListResponse<NFTActionTypesData>>(endpoint);
            }
            public async Task<PaginatedResponse<NFTTokenOwnershipData>> GetContractPackageNFTOwnershipAsync(string contractPackageHash, NFTContractPackageOwnershipRequestParameters parameters = null)
            {
                string endpoint = Endpoints.NFT.GetContractPackageNFTOwnership(_baseUrl, contractPackageHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<NFTTokenOwnershipData>>(endpoint);
            }
            public async Task<PaginatedResponse<NFTTokenOwnershipData>> GetAccountNFTOwnershipAsync(string accountIdentifier, NFTAccountOwnershipRequestParameters parameters = null)
            {
                string endpoint = Endpoints.NFT.GetAccountNFTOwnership(_baseUrl, accountIdentifier, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<NFTTokenOwnershipData>>(endpoint);
            }
            public async Task<Response<RateData>> GetCurrentCurrencyRateAsync(string currencyId)
            {
                string endpoint = Endpoints.Rate.GetCurrentCurrencyRate(_baseUrl, currencyId);
                return await _casperCloudRestClient.GetDataAsync<Response<RateData>>(endpoint);
            }
            public async Task<PaginatedResponse<RateData>> GetHistoricalCurrencyRatesAsync(string currencyId, RateHistoricalRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Rate.GetHistoricalCurrencyRates(_baseUrl, currencyId, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<RateData>>(endpoint);
            }
            public async Task<PaginatedResponse<CurrencyData>> GetCurrenciesAsync(RateCurrenciesRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Rate.GetCurrencies(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<CurrencyData>>(endpoint);
            }
            public async Task<Response<SupplyData>> GetSupplyAsync()
            {
                string endpoint = Endpoints.Supply.GetSupply(_baseUrl);
                return await _casperCloudRestClient.GetDataAsync<Response<SupplyData>>(endpoint);
            }
            public async Task<PaginatedResponse<TransferData>> GetAccountTransfersAsync(string accountIdentifier, TransferAccountRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Transfer.GetAccountTransfers(_baseUrl, accountIdentifier, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<TransferData>>(endpoint);
            }
            public async Task<PaginatedResponse<TransferData>> GetDeployTransfersAsync(string deployHash, TransferDeployRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Transfer.GetDeployTransfers(_baseUrl, deployHash, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<TransferData>>(endpoint);
            }
            public async Task<Response<ValidatorData>> GetValidatorAsync(string publicKey, ValidatorRequestParameters parameters)
            {
                string endpoint = Endpoints.Validator.GetValidator(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<Response<ValidatorData>>(endpoint);
            }
            public async Task<PaginatedResponse<ValidatorData>> GetValidatorsAsync(ValidatorsRequestParameters parameters)
            {
                string endpoint = Endpoints.Validator.GetValidators(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<ValidatorData>>(endpoint);
            }
            public async Task<PaginatedResponse<RelativeValidatorPerformanceData>> GetHistoricalValidatorPerformanceAsync(string publicKey, ValidatorHistoricalPerformanceRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Validator.GetHistoricalValidatorPerformance(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<RelativeValidatorPerformanceData>>(endpoint);
            }
            public async Task<PaginatedResponse<ValidatorPerformanceData>> GetHistoricalValidatorAveragePerformanceAsync(string publicKey, ValidatorHistoricalAveragePerformanceRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Validator.GetHistoricalAverageValidatorPerformance(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<ValidatorPerformanceData>>(endpoint);
            }
            public async Task<PaginatedResponse<RelativeValidatorPerformanceData>> GetHistoricalValidatorsAveragePerformanceAsync(ValidatorsHistoricalAveragePerformanceRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Validator.GetHistoricalAverageValidatorsPerformance(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<RelativeValidatorPerformanceData>>(endpoint);
            }
            public async Task<PaginatedResponse<ValidatorRewardData>> GetValidatorRewardsAsync(string publicKey, ValidatorRewardsRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Validator.GetValidatorRewards(_baseUrl, publicKey, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<ValidatorRewardData>>(endpoint);
            }
            public async Task<Response<ulong>> GetValidatorTotalRewardsAsync(string publicKey)
            {
                string endpoint = Endpoints.Validator.GetValidatorTotalRewards(_baseUrl, publicKey);
                return await _casperCloudRestClient.GetDataAsync<Response<ulong>>(endpoint);
            }
            public async Task<Response<AccountInfoData>> GetAccountInfoAsync(string accountHash)
            {
                string endpoint = Endpoints.Account.GetAccountInfo(_baseUrl, accountHash);
                return await _casperCloudRestClient.GetDataAsync<Response<AccountInfoData>>(endpoint);
            }
            public async Task<PaginatedResponse<AccountInfoData>> GetAccountInfosAsync(AccountInfosRequestParameters parameters = null)
            {
                string endpoint = Endpoints.Account.GetAccountInfos(_baseUrl, parameters);
                return await _casperCloudRestClient.GetDataAsync<PaginatedResponse<AccountInfoData>>(endpoint);
            }
            public async Task<Response<AuctionMetricsData>> GetAuctionMetricsAsync()
            {
                string endpoint = Endpoints.Auction.GetAuctionMetrics(_baseUrl);
                return await _casperCloudRestClient.GetDataAsync<Response<AuctionMetricsData>>(endpoint);
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
            /// <returns>The account data.</returns>
            public Task<AccountData> GetAccountAsync(string publicKey, AccountsOptionalParameters parameters = null)
            {
                return _commonEndpoint.GetAccountAsync(publicKey, parameters);
            }
            /// <summary>
            /// Gets a paginated list of accounts
            /// <para>For more information, see <see href="https://docs.cspr.cloud/rest-api/account/get-accounts">CSPR.Cloud API documentation</see>.</para>
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A paginated response containing account data.</returns>
            public Task<PaginatedResponse<AccountData>> GetAccountsAsync(AccountsRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountsAsync(parameters);
            }
            /// <summary>
            /// Get account info by account hash
            /// <para>For more information, see <see href="https://docs.cspr.cloud/rest-api/account-info/get-account-info">CSPR.Cloud API documentation</see>.</para>
            /// </summary>
            /// <param name="accountHash">The hash of the account.</param>
            /// <returns>A response containing account info data.</returns>
            public Task<Response<AccountInfoData>> GetAccountInfoAsync(string accountHash)
            {
                return _commonEndpoint.GetAccountInfoAsync(accountHash);
            }
            /// <summary>
            /// Get account infos
            /// <para>For more information, see <see href="https://docs.cspr.cloud/rest-api/account-info/get-account-infos">CSPR.Cloud API documentation</see>.</para>
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A paginated response containing account info data.</returns>
            public Task<PaginatedResponse<AccountInfoData>> GetAccountInfosAsync(AccountInfosRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountInfosAsync(parameters);
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
            /// <returns>A singular response containing auction metrics data.</returns>
            public Task<Response<AuctionMetricsData>> GetAuctionMetricsAsync()
            {
                return _commonEndpoint.GetAuctionMetricsAsync();
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
            /// <returns>A task that represents the asynchronous operation. The task result contains the block data.</returns>
            public Task<BlockData> GetBlockAsync(string blockHash, BlockOptionalParameters parameters = null)
            {
                return _commonEndpoint.GetBlockAsync(blockHash, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of blocks asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/block/get-blocks">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of block data.</returns>
            public Task<PaginatedResponse<BlockData>> GetBlocksAsync(BlockRequestParameters parameters = null)
            {
                return _commonEndpoint.GetBlocksAsync(parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of blocks proposed by a specific validator asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/block/get-validator-blocks">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="validatorPublicKey">The public key of the validator whose blocks to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of block data.</returns>
            public Task<PaginatedResponse<BlockData>> GetValidatorBlocksAsync(string validatorPublicKey, BlockRequestParameters parameters = null)
            {
                return _commonEndpoint.GetValidatorBlocksAsync(validatorPublicKey, parameters);
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
            /// <returns>A task that represents the asynchronous operation. The task result contains the bidder data.</returns>
            public Task<BidderData> GetBidderAsync(string publicKey, BidderRequestParameters parameters)
            {
                return _commonEndpoint.GetBidderAsync(publicKey, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of bidders asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/bidder/get-bidders">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of bidder data.</returns>
            public Task<PaginatedResponse<BidderData>> GetBiddersAsync(BiddersRequestParameters parameters)
            {
                return _commonEndpoint.GetBiddersAsync(parameters);
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
            /// <returns>A task that represents the asynchronous operation. The task result contains the centralized account information data.</returns>
            public Task<CentralizedAccountInfoData> GetCentralizedAccountInfoAsync(string accountHash)
            {
                return _commonEndpoint.GetCentralizedAccountInfoAsync(accountHash);
            }

            /// <summary>
            /// Retrieves a paginated list of centralized account information asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/centralized-account-info/get-centralized-account-infos">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of centralized account information data.</returns>
            public Task<PaginatedResponse<CentralizedAccountInfoData>> GetCentralizedAccountInfosAsync(CentralizedAccountInfoRequestParameters parameters)
            {
                return _commonEndpoint.GetCentralizedAccountInfosAsync(parameters);
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
            /// <returns>A task that represents the asynchronous operation. The task result contains the contract data.</returns>
            public Task<ContractData> GetContractAsync(string contractHash, ContractRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractAsync(contractHash, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of contracts asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract/get-contracts">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of contract data.</returns>
            public Task<PaginatedResponse<ContractData>> GetContractsAsync(ContractsRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractsAsync(parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of contracts by contract package hash asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract/get-contracts-by-contract-package">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package to retrieve contracts for.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of contract data.</returns>
            public Task<PaginatedResponse<ContractData>> GetContractsByContractPackageAsync(string contractPackageHash, ByContractRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractsByContractPackageAsync(contractPackageHash, parameters);
            }

            /// <summary>
            /// Retrieves a list of contract types asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract/get-contract-types">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <returns>A task that represents the asynchronous operation. The task result contains a list of contract type data.</returns>
            public Task<List<ContractTypeData>> GetContractTypesAsync()
            {
                return _commonEndpoint.GetContractTypesAsync();
            }

            /// <summary>
            /// Retrieves a paginated list of contract entry points asynchronously by contract hash.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract-entry-point/get-contract-entry-points">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractHash">The hash of the contract to retrieve entry points for.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of entry point data.</returns>
            public Task<PaginatedResponse<EntryPointData>> GetContractEntryPointsAsync(string contractHash)
            {
                return _commonEndpoint.GetContractEntryPointsAsync(contractHash);
            }

            /// <summary>
            /// Retrieves the cost of a specific contract entry point asynchronously by contract hash and entry point name.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract-entry-point/get-contract-entry-point-costs">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractHash">The hash of the contract to retrieve entry point costs for.</param>
            /// <param name="entryPointName">The name of the entry point to retrieve costs for.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the entry point cost data.</returns>
            public Task<Response<EntryPointCostData>> GetContractEntryPointCostsAsync(string contractHash, string entryPointName)
            {
                return _commonEndpoint.GetContractEntryPointCostsAsync(contractHash, entryPointName);
            }

            /// <summary>
            /// Retrieves a contract package asynchronously by its hash.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract-package/get-contract-package">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package to retrieve.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the contract package data.</returns>
            public Task<ContractResponse<ContractPackageData>> GetContractPackageAsync(string contractPackageHash)
            {
                return _commonEndpoint.GetContractPackageAsync(contractPackageHash);
            }

            /// <summary>
            /// Retrieves a paginated list of contract packages asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract-package/get-contract-packages">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of contract package data.</returns>
            public Task<PaginatedResponse<ContractPackageData>> GetContractPackagesAsync(ContractPackageRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackagesAsync(parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of contract packages associated with a specific account asynchronously by public key.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/contract-package/get-account-contract-packages">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the account to retrieve contract packages for.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of contract package data.</returns>
            public Task<PaginatedResponse<ContractPackageData>> GetAccountContractPackagesAsync(string publicKey, AccountContractPackageRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountContractPackagesAsync(publicKey, parameters);
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
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of delegation data.</returns>
            public Task<PaginatedResponse<DelegationData>> GetAccountDelegationsAsync(string publicKey, DelegationRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountDelegationsAsync(publicKey, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of delegations for a specific validator asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/delegation/get-validator-delegations">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the validator whose delegations to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of delegation data.</returns>
            public Task<PaginatedResponse<DelegationData>> GetValidatorDelegationsAsync(string publicKey, DelegationRequestParameters parameters = null)
            {
                return _commonEndpoint.GetValidatorDelegationsAsync(publicKey, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of delegator rewards for a specific account asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/delegator-reward/get-account-delegation-rewards">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the account whose delegator rewards to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of delegator reward data.</returns>
            public Task<PaginatedResponse<DelegatorRewardData>> GetAccountDelegatorRewardsAsync(string publicKey, AccountDelegatorRewardRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountDelegatorRewardsAsync(publicKey, parameters);
            }

            /// <summary>
            /// Retrieves the total delegation rewards for a specific account asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/delegator-reward/get-account-total-delegation-rewards">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the account whose total delegation rewards to retrieve.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the total delegation rewards amount.</returns>
            public Task<ulong> GetTotalAccountDelegationRewards(string publicKey)
            {
                return _commonEndpoint.GetTotalAccountDelegationRewards(publicKey);
            }

            /// <summary>
            /// Retrieves the total delegation rewards for a specific validator asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/delegator-reward/get-total-validator-delegators-rewards">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the validator whose total delegation rewards to retrieve.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the total delegation rewards amount.</returns>
            public Task<ulong> GetTotalValidatorDelegationRewards(string publicKey)
            {
                return _commonEndpoint.GetTotalValidatorDelegationRewards(publicKey);
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
            /// <returns>A task that represents the asynchronous operation. The task result contains the deploy data.</returns>
            public Task<Response<DeployData>> GetDeployAsync(string deployHash, DeployRequestParameters parameters = null)
            {
                return _commonEndpoint.GetDeployAsync(deployHash, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of deploys asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/deploy/get-deploys">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of deploy data.</returns>
            public Task<PaginatedResponse<DeployData>> GetDeploysAsync(DeploysRequestParameters parameters = null)
            {
                return _commonEndpoint.GetDeploysAsync(parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of deploys associated with a specific account asynchronously by public key.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/deploy/get-account-deploys">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the account whose deploys to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of deploy data.</returns>
            public Task<PaginatedResponse<DeployData>> GetAccountDeploysAsync(string publicKey, AccountDeploysRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountDeploysAsync(publicKey, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of deploys included in a specific block asynchronously by block identifier.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/deploy/get-block-deploys">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="blockIdentifier">The identifier of the block whose deploys to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of deploy data.</returns>
            public Task<PaginatedResponse<DeployData>> GetBlockDeploysAsync(string blockIdentifier, BlockDeploysRequestParameters parameters = null)
            {
                return _commonEndpoint.GetBlockDeploysAsync(blockIdentifier, parameters);
            }

            /// <summary>
            /// Retrieves a list of deploy execution types asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/deploy/get-deploy-execution-types">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <returns>A task that represents the asynchronous operation. The task result contains a list of deploy execution types data.</returns>
            public Task<Response<List<DeployExecutionTypesData>>> GetDeployExecutionTypesAsync()
            {
                return _commonEndpoint.GetDeployExecutionTypesAsync();
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
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of fungible token action data.</returns>
            public Task<PaginatedResponse<FTTokenActionData>> GetFTActionsAsync(FTActionRequestParameters parameters = null)
            {
                return _commonEndpoint.GetFungibleTokenActionsAsync(parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of fungible token actions for a specific account asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/fungible-token-action/get-account-token-actions">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="accountIdentifier">The identifier of the account whose fungible token actions to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of fungible token action data.</returns>
            public Task<PaginatedResponse<FTTokenActionData>> GetAccountFTActionsAsync(string accountIdentifier, FTAccountActionRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountFungibleTokenActionsAsync(accountIdentifier, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of fungible token actions for a specific contract package asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/fungible-token-action/get-contract-package-token-actions">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package whose fungible token actions to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of fungible token action data.</returns>
            public Task<PaginatedResponse<FTTokenActionData>> GetContractPackageFTActionsAsync(string contractPackageHash, FTContractPackageActionRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackageFungibleTokenActionsAsync(contractPackageHash, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of fungible token ownership data for a specific account asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/fungible-token-ownership/get-account-fungible-token-ownership">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="accountIdentifier">The identifier of the account whose fungible token ownership data to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of fungible token ownership data.</returns>
            public Task<PaginatedResponse<FTOwnershipData>> GetAccountFTOwnershipAsync(string accountIdentifier, FTAccountOwnershipRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountFungibleTokenOwnershipAsync(accountIdentifier, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of fungible token ownership data for a specific contract package asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/fungible-token-ownership/get-contract-package-fungible-token-ownership">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package whose fungible token ownership data to retrieve.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of fungible token ownership data.</returns>
            public Task<PaginatedResponse<FTOwnershipData>> GetContractPackageFTOwnershipAsync(string contractPackageHash)
            {
                return _commonEndpoint.GetContractPackageFungibleTokenOwnershipAsync(contractPackageHash);
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
            /// <returns>A task that represents the asynchronous operation. The task result contains the NFT token data.</returns>
            public Task<Response<NFTTokenData>> GetNFTAsync(string contractPackageHash, string tokenId, NFTRequestParameters parameters = null)
            {
                return _commonEndpoint.GetNFTAsync(contractPackageHash, tokenId, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of NFTs for a specific account asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token/get-account-tokens">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="accountIdentifier">The identifier of the account whose NFTs to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of NFT token data.</returns>
            public Task<PaginatedResponse<NFTTokenData>> GetAccountNFTsAsync(string accountIdentifier, NFTAccountRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountNFTsAsync(accountIdentifier, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of NFTs for a specific contract package asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token/get-contract-package-tokens">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package whose NFTs to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of NFT token data.</returns>
            public Task<PaginatedResponse<NFTTokenData>> GetContractPackageNFTsAsync(string contractPackageHash, NFTContractPackageRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackageNFTsAsync(contractPackageHash, parameters);
            }

            /// <summary>
            /// Retrieves a list of NFT standards asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token/get-token-standards">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <returns>A task that represents the asynchronous operation. The task result contains a list response of NFT standard data.</returns>
            public Task<ListResponse<NFTStandardData>> GetNFTStandardsAsync()
            {
                return _commonEndpoint.GetNFTStandardsAsync();
            }

            /// <summary>
            /// Retrieves a list of offchain NFT metadata statuses asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token/get-token-offchain-metadata-statuses">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <returns>A task that represents the asynchronous operation. The task result contains a list response of NFT metadata status data.</returns>
            public Task<ListResponse<NFTMetadataStatusData>> GetOffchainNFTMetadataStatusesAsync()
            {
                return _commonEndpoint.GetOffchainNFTMetadataStatusesAsync();
            }

            /// <summary>
            /// Retrieves a paginated list of NFT actions for a specific contract package and token asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token-action/get-contract-package-token-actions-by-token">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package.</param>
            /// <param name="tokenId">The ID of the token whose actions to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of NFT token action data.</returns>
            public Task<PaginatedResponse<NFTTokenActionData>> GetContractPackageNFTActionsForATokenAsync(string contractPackageHash, string tokenId, NFTContractPackageTokenActionsRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackageNFTActionsForATokenAsync(contractPackageHash, tokenId, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of NFT actions for a specific account asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token-action/get-account-token-actions">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="accountIdentifier">The identifier of the account whose NFT actions to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of NFT token action data.</returns>
            public Task<PaginatedResponse<NFTTokenActionData>> GetAccountNFTActionsAsync(string accountIdentifier, NFTAccountActionsRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountNFTActionsAsync(accountIdentifier, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of NFT actions for a specific contract package asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token-action/get-contract-package-token-actions">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package whose NFT actions to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of NFT token action data.</returns>
            public Task<PaginatedResponse<NFTTokenActionData>> GetContractPackageNFTActionsAsync(string contractPackageHash, NFTContractPackageActionsRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackageNFTActionsAsync(contractPackageHash, parameters);
            }

            /// <summary>
            /// Retrieves a list of NFT action types asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token-action/get-token-actions-types">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <returns>A task that represents the asynchronous operation. The task result contains a list response of NFT action types data.</returns>
            public Task<ListResponse<NFTActionTypesData>> GetNFTActionTypesAsync()
            {
                return _commonEndpoint.GetNFTActionTypesAsync();
            }

            /// <summary>
            /// Retrieves a paginated list of NFT ownership data for a specific contract package asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token-ownership/get-contract-package-token-ownership">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="contractPackageHash">The hash of the contract package whose NFT ownership data to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of NFT token ownership data.</returns>
            public Task<PaginatedResponse<NFTTokenOwnershipData>> GetContractPackageNFTOwnershipAsync(string contractPackageHash, NFTContractPackageOwnershipRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackageNFTOwnershipAsync(contractPackageHash, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of NFT ownership data for a specific account asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/non-fungible-token-ownership/get-account-token-ownership">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="accountIdentifier">The identifier of the account whose NFT ownership data to retrieve.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of NFT token ownership data.</returns>
            public Task<PaginatedResponse<NFTTokenOwnershipData>> GetAccountNFTOwnershipAsync(string accountIdentifier, NFTAccountOwnershipRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountNFTOwnershipAsync(accountIdentifier, parameters);
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
            /// <returns>A task that represents the asynchronous operation. The task result contains the current rate data.</returns>
            public Task<Response<RateData>> GetCurrentCurrencyRateAsync(string currencyId)
            {
                return _commonEndpoint.GetCurrentCurrencyRateAsync(currencyId);
            }

            /// <summary>
            /// Retrieves historical currency rates asynchronously for a specified currency ID.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/cspr-rate/get-historical-currency-rates">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="currencyId">The ID of the currency for which to retrieve historical rates.</param>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of historical rate data.</returns>
            public Task<PaginatedResponse<RateData>> GetHistoricalCurrencyRatesAsync(string currencyId, RateHistoricalRequestParameters parameters = null)
            {
                return _commonEndpoint.GetHistoricalCurrencyRatesAsync(currencyId, parameters);
            }

            /// <summary>
            /// Retrieves a paginated list of currencies asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/cspr-rate/get-currencies">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters for the request.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of currency data.</returns>
            public Task<PaginatedResponse<CurrencyData>> GetCurrenciesAsync(RateCurrenciesRequestParameters parameters = null)
            {
                return _commonEndpoint.GetCurrenciesAsync(parameters);
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
            /// <returns>A task that represents the asynchronous operation. The task result contains the current supply data.</returns>
            public Task<Response<SupplyData>> GetSupplyAsync()
            {
                return _commonEndpoint.GetSupplyAsync();
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
            /// <returns>A task that represents the asynchronous operation. The task result contains paginated transfer data for the account.</returns>
            public Task<PaginatedResponse<TransferData>> GetAccountTransfersAsync(string accountIdentifier, TransferAccountRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountTransfersAsync(accountIdentifier, parameters);
            }

            /// <summary>
            /// Retrieves deploy transfers asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/transfer/get-deploy-transfers">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="deployHash">The hash of the deploy for which transfers are to be retrieved.</param>
            /// <param name="parameters">Optional parameters to filter or paginate results.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains paginated transfer data for the deploy.</returns>
            public Task<PaginatedResponse<TransferData>> GetDeployTransfersAsync(string deployHash, TransferDeployRequestParameters parameters = null)
            {
                return _commonEndpoint.GetDeployTransfersAsync(deployHash, parameters);
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
            /// <returns>A task that represents the asynchronous operation. The task result contains details of the validator.</returns>
            public Task<Response<ValidatorData>> GetValidatorAsync(string publicKey, ValidatorRequestParameters parameters)
            {
                return _commonEndpoint.GetValidatorAsync(publicKey, parameters);
            }

            /// <summary>
            /// Retrieves validators asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator/get-validators">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters to filter or paginate results.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a list of validators.</returns>
            public Task<PaginatedResponse<ValidatorData>> GetValidatorsAsync(ValidatorsRequestParameters parameters)
            {
                return _commonEndpoint.GetValidatorsAsync(parameters);
            }

            /// <summary>
            /// Retrieves historical performance of a validator asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator-performance/get-historical-validator-performance">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the validator for which historical performance is to be retrieved.</param>
            /// <param name="parameters">Optional parameters to filter or paginate results.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains historical performance data of the validator.</returns>
            public Task<PaginatedResponse<RelativeValidatorPerformanceData>> GetHistoricalValidatorPerformanceAsync(string publicKey, ValidatorHistoricalPerformanceRequestParameters parameters = null)
            {
                return _commonEndpoint.GetHistoricalValidatorPerformanceAsync(publicKey, parameters);
            }

            /// <summary>
            /// Retrieves historical average performance of a validator asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator-performance/get-historical-average-validator-performance">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the validator for which historical average performance is to be retrieved.</param>
            /// <param name="parameters">Optional parameters to filter or paginate results.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains historical average performance data of the validator.</returns>
            public Task<PaginatedResponse<ValidatorPerformanceData>> GetHistoricalValidatorAveragePerformanceAsync(string publicKey, ValidatorHistoricalAveragePerformanceRequestParameters parameters = null)
            {
                return _commonEndpoint.GetHistoricalValidatorAveragePerformanceAsync(publicKey, parameters);
            }

            /// <summary>
            /// Retrieves historical average performance of validators asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator-performance/get-historical-average-validators-performance">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="parameters">Optional parameters to filter or paginate results.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains historical average performance data of validators.</returns>
            public Task<PaginatedResponse<RelativeValidatorPerformanceData>> GetHistoricalValidatorsAveragePerformanceAsync(ValidatorsHistoricalAveragePerformanceRequestParameters parameters = null)
            {
                return _commonEndpoint.GetHistoricalValidatorsAveragePerformanceAsync(parameters);
            }

            /// <summary>
            /// Retrieves validator rewards asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator-reward/get-validator-rewards">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the validator for which rewards are to be retrieved.</param>
            /// <param name="parameters">Optional parameters to filter or paginate results.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains validator rewards data.</returns>
            public Task<PaginatedResponse<ValidatorRewardData>> GetValidatorRewardsAsync(string publicKey, ValidatorRewardsRequestParameters parameters = null)
            {
                return _commonEndpoint.GetValidatorRewardsAsync(publicKey, parameters);
            }

            /// <summary>
            /// Retrieves total rewards earned by a validator asynchronously.
            /// For more information, see <see href="https://docs.cspr.cloud/rest-api/validator-reward/get-validator-total-rewards">CSPR Cloud API documentation</see>.
            /// </summary>
            /// <param name="publicKey">The public key of the validator for which total rewards are to be retrieved.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains the total rewards earned by the validator.</returns>
            public Task<Response<ulong>> GetValidatorTotalRewardsAsync(string publicKey)
            {
                return _commonEndpoint.GetValidatorTotalRewardsAsync(publicKey);
            }
        }




    }
}