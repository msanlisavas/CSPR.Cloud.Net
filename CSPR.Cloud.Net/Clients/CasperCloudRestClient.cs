using CSPR.Cloud.Net.Clients.Api;
using CSPR.Cloud.Net.Errors;
using CSPR.Cloud.Net.Interfaces.Clients;
using CSPR.Cloud.Net.Objects.Abstract;
using CSPR.Cloud.Net.Objects.Account;
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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSPR.Cloud.Net.Clients
{
    public class CasperCloudRestClient : ICasperCloudRestClient
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
            public Transfer Transfer { get; }
            public MainnetEndpoint(CasperCloudRestClient casperCloudRestClient)
            {
                _commonEndpoint = new CommonEndpoint(casperCloudRestClient, Endpoints.BaseUrls.Mainnet);
                Transfer = new Transfer(_commonEndpoint);
            }
            public Task<AccountData> GetAccountAsync(string publicKey, AccountsOptionalParameters parameters)
            {
                return _commonEndpoint.GetAccountAsync(publicKey, parameters);
            }
            public Task<PaginatedResponse<AccountData>> GetAccountsAsync(AccountsRequestParameters parameters)
            {
                return _commonEndpoint.GetAccountsAsync(parameters);
            }
            public Task<BlockData> GetBlockAsync(string blockHash, BlockOptionalParameters parameters = null)
            {
                return _commonEndpoint.GetBlockAsync(blockHash, parameters);
            }
            public Task<PaginatedResponse<BlockData>> GetBlocksAsync(BlockRequestParameters parameters = null)
            {
                return _commonEndpoint.GetBlocksAsync(parameters);
            }
            public Task<PaginatedResponse<BlockData>> GetValidatorBlocksAsync(string validatorPublicKey, BlockRequestParameters parameters = null)
            {
                return _commonEndpoint.GetValidatorBlocksAsync(validatorPublicKey, parameters);
            }
            public Task<BidderData> GetBidderAsync(string publicKey, BidderRequestParameters parameters)
            {
                return _commonEndpoint.GetBidderAsync(publicKey, parameters);
            }
            public Task<PaginatedResponse<BidderData>> GetBiddersAsync(BiddersRequestParameters parameters)
            {
                return _commonEndpoint.GetBiddersAsync(parameters);
            }
            public Task<CentralizedAccountInfoData> GetCentralizedAccountInfoAsync(string accountHash)
            {
                return _commonEndpoint.GetCentralizedAccountInfoAsync(accountHash);
            }
            public Task<PaginatedResponse<CentralizedAccountInfoData>> GetCentralizedAccountInfosAsync(CentralizedAccountInfoRequestParameters parameters)
            {
                return _commonEndpoint.GetCentralizedAccountInfosAsync(parameters);
            }
            public Task<ContractData> GetContractAsync(string contractHash, ContractRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractAsync(contractHash, parameters);
            }
            public Task<PaginatedResponse<ContractData>> GetContractsAsync(ContractsRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractsAsync(parameters);
            }
            public Task<PaginatedResponse<ContractData>> GetContractsByContractPackageAsync(string contractPackageHash, ByContractRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractsByContractPackageAsync(contractPackageHash, parameters);
            }
            public Task<List<ContractTypeData>> GetContractTypesAsync()
            {
                return _commonEndpoint.GetContractTypesAsync();
            }
            public Task<PaginatedResponse<EntryPointData>> GetContractEntryPointsAsync(string contractHash)
            {
                return _commonEndpoint.GetContractEntryPointsAsync(contractHash);
            }
            public Task<Response<EntryPointCostData>> GetContractEntryPointCostsAsync(string contractHash, string entryPointName)
            {
                return _commonEndpoint.GetContractEntryPointCostsAsync(contractHash, entryPointName);
            }
            public Task<ContractResponse<ContractPackageData>> GetContractPackageAsync(string contractPackageHash)
            {
                return _commonEndpoint.GetContractPackageAsync(contractPackageHash);
            }
            public Task<PaginatedResponse<ContractPackageData>> GetContractPackagesAsync(ContractPackageRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackagesAsync(parameters);
            }
            public Task<PaginatedResponse<ContractPackageData>> GetAccountContractPackagesAsync(string publicKey, AccountContractPackageRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountContractPackagesAsync(publicKey, parameters);
            }
            public Task<PaginatedResponse<DelegationData>> GetAccountDelegationsAsync(string publicKey, DelegationRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountDelegationsAsync(publicKey, parameters);
            }
            public Task<PaginatedResponse<DelegationData>> GetValidatorDelegationsAsync(string publicKey, DelegationRequestParameters parameters = null)
            {
                return _commonEndpoint.GetValidatorDelegationsAsync(publicKey, parameters);
            }
            public Task<PaginatedResponse<DelegatorRewardData>> GetAccountDelegatorRewardsAsync(string publicKey, AccountDelegatorRewardRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountDelegatorRewardsAsync(publicKey, parameters);
            }
            public Task<ulong> GetTotalAccountDelegationRewards(string publicKey)
            {
                return _commonEndpoint.GetTotalAccountDelegationRewards(publicKey);
            }
            public Task<ulong> GetTotalValidatorDelegationRewards(string publicKey)
            {
                return _commonEndpoint.GetTotalValidatorDelegationRewards(publicKey);
            }
            public Task<Response<DeployData>> GetDeployAsync(string deployHash, DeployRequestParameters parameters = null)
            {
                return _commonEndpoint.GetDeployAsync(deployHash, parameters);
            }
            public Task<PaginatedResponse<DeployData>> GetDeploysAsync(DeploysRequestParameters parameters = null)
            {
                return _commonEndpoint.GetDeploysAsync(parameters);
            }
            public Task<PaginatedResponse<DeployData>> GetAccountDeploysAsync(string publicKey, AccountDeploysRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountDeploysAsync(publicKey, parameters);
            }
            public Task<PaginatedResponse<DeployData>> GetBlockDeploysAsync(string blockIdentifier, BlockDeploysRequestParameters parameters = null)
            {
                return _commonEndpoint.GetBlockDeploysAsync(blockIdentifier, parameters);
            }
            public Task<Response<List<DeployExecutionTypesData>>> GetDeployExecutionTypesAsync()
            {
                return _commonEndpoint.GetDeployExecutionTypesAsync();
            }
            public Task<PaginatedResponse<FTTokenActionData>> GetFungibleTokenActionsAsync(FTActionRequestParameters parameters = null)
            {
                return _commonEndpoint.GetFungibleTokenActionsAsync(parameters);
            }
            public Task<PaginatedResponse<FTTokenActionData>> GetAccountFungibleTokenActionsAsync(string accountIdentifier, FTAccountActionRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountFungibleTokenActionsAsync(accountIdentifier, parameters);
            }
            public Task<PaginatedResponse<FTTokenActionData>> GetContractPackageFungibleTokenActionsAsync(string contractPackageHash, FTContractPackageActionRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackageFungibleTokenActionsAsync(contractPackageHash, parameters);
            }
            public Task<PaginatedResponse<FTOwnershipData>> GetAccountFungibleTokenOwnershipAsync(string accountIdentifier, FTAccountOwnershipRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountFungibleTokenOwnershipAsync(accountIdentifier, parameters);
            }
            public Task<PaginatedResponse<FTOwnershipData>> GetContractPackageFungibleTokenOwnershipAsync(string contractPackageHash)
            {
                return _commonEndpoint.GetContractPackageFungibleTokenOwnershipAsync(contractPackageHash);
            }
            public Task<Response<NFTTokenData>> GetNonFungibleTokenAsync(string contractPackageHash, string tokenId, NFTRequestParameters parameters = null)
            {
                return _commonEndpoint.GetNFTAsync(contractPackageHash, tokenId, parameters);
            }
            public Task<PaginatedResponse<NFTTokenData>> GetAccountNFTsAsync(string accountIdentifier, NFTAccountRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountNFTsAsync(accountIdentifier, parameters);
            }
            public Task<PaginatedResponse<NFTTokenData>> GetContractPackageNFTsAsync(string contractPackageHash, NFTContractPackageRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackageNFTsAsync(contractPackageHash, parameters);
            }
            public Task<ListResponse<NFTStandardData>> GetNFTStandardsAsync()
            {
                return _commonEndpoint.GetNFTStandardsAsync();
            }
            public Task<ListResponse<NFTMetadataStatusData>> GetOffchainNFTMetadataStatusesAsync()
            {
                return _commonEndpoint.GetOffchainNFTMetadataStatusesAsync();
            }
            public Task<PaginatedResponse<NFTTokenActionData>> GetContractPackageNFTActionsForATokenAsync(string contractPackageHash, string tokenId, NFTContractPackageTokenActionsRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackageNFTActionsForATokenAsync(contractPackageHash, tokenId, parameters);
            }
            public Task<PaginatedResponse<NFTTokenActionData>> GetAccountNFTActionsAsync(string accountIdentifier, NFTAccountActionsRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountNFTActionsAsync(accountIdentifier, parameters);
            }
            public Task<PaginatedResponse<NFTTokenActionData>> GetContractPackageNFTActionsAsync(string contractPackageHash, NFTContractPackageActionsRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackageNFTActionsAsync(contractPackageHash, parameters);
            }
            public Task<ListResponse<NFTActionTypesData>> GetNFTActionTypesAsync()
            {
                return _commonEndpoint.GetNFTActionTypesAsync();
            }
            public Task<PaginatedResponse<NFTTokenOwnershipData>> GetContractPackageNFTOwnershipAsync(string contractPackageHash, NFTContractPackageOwnershipRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackageNFTOwnershipAsync(contractPackageHash, parameters);
            }
            public Task<PaginatedResponse<NFTTokenOwnershipData>> GetAccountNFTOwnershipAsync(string accountIdentifier, NFTAccountOwnershipRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountNFTOwnershipAsync(accountIdentifier, parameters);
            }
            public Task<Response<RateData>> GetCurrentCurrencyRateAsync(string currencyId)
            {
                return _commonEndpoint.GetCurrentCurrencyRateAsync(currencyId);
            }
            public Task<PaginatedResponse<RateData>> GetHistoricalCurrencyRatesAsync(string currencyId, RateHistoricalRequestParameters parameters = null)
            {
                return _commonEndpoint.GetHistoricalCurrencyRatesAsync(currencyId, parameters);
            }
            public Task<PaginatedResponse<CurrencyData>> GetCurrenciesAsync(RateCurrenciesRequestParameters parameters = null)
            {
                return _commonEndpoint.GetCurrenciesAsync(parameters);
            }
            public Task<Response<SupplyData>> GetSupplyAsync()
            {
                return _commonEndpoint.GetSupplyAsync();
            }
        }
        public class TestnetEndpoint
        {
            private readonly CommonEndpoint _commonEndpoint;
            public Transfer Transfer { get; }
            public TestnetEndpoint(CasperCloudRestClient casperCloudRestClient)
            {
                _commonEndpoint = new CommonEndpoint(casperCloudRestClient, Endpoints.BaseUrls.Testnet);
                Transfer = new Transfer(_commonEndpoint);
            }

            public Task<AccountData> GetAccountAsync(string publicKey, AccountsOptionalParameters parameters = null)
            {
                return _commonEndpoint.GetAccountAsync(publicKey, parameters);
            }
            public Task<PaginatedResponse<AccountData>> GetAccountsAsync(AccountsRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountsAsync(parameters);
            }
            public Task<BlockData> GetBlockAsync(string blockHash, BlockOptionalParameters parameters = null)
            {
                return _commonEndpoint.GetBlockAsync(blockHash, parameters);
            }
            public Task<PaginatedResponse<BlockData>> GetBlocksAsync(BlockRequestParameters parameters = null)
            {
                return _commonEndpoint.GetBlocksAsync(parameters);
            }
            public Task<PaginatedResponse<BlockData>> GetValidatorBlocksAsync(string validatorPublicKey, BlockRequestParameters parameters = null)
            {
                return _commonEndpoint.GetValidatorBlocksAsync(validatorPublicKey, parameters);
            }
            public Task<BidderData> GetBidderAsync(string publicKey, BidderRequestParameters parameters)
            {
                return _commonEndpoint.GetBidderAsync(publicKey, parameters);
            }
            public Task<PaginatedResponse<BidderData>> GetBiddersAsync(BiddersRequestParameters parameters)
            {
                return _commonEndpoint.GetBiddersAsync(parameters);
            }
            public Task<CentralizedAccountInfoData> GetCentralizedAccountInfoAsync(string accountHash)
            {
                return _commonEndpoint.GetCentralizedAccountInfoAsync(accountHash);
            }
            public Task<PaginatedResponse<CentralizedAccountInfoData>> GetCentralizedAccountInfosAsync(CentralizedAccountInfoRequestParameters parameters)
            {
                return _commonEndpoint.GetCentralizedAccountInfosAsync(parameters);
            }
            public Task<ContractData> GetContractAsync(string contractHash, ContractRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractAsync(contractHash, parameters);
            }
            public Task<PaginatedResponse<ContractData>> GetContractsAsync(ContractsRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractsAsync(parameters);
            }
            public Task<PaginatedResponse<ContractData>> GetContractsByContractPackageAsync(string contractPackageHash, ByContractRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractsByContractPackageAsync(contractPackageHash, parameters);
            }
            public Task<List<ContractTypeData>> GetContractTypesAsync()
            {
                return _commonEndpoint.GetContractTypesAsync();
            }
            public Task<PaginatedResponse<EntryPointData>> GetContractEntryPointsAsync(string contractHash)
            {
                return _commonEndpoint.GetContractEntryPointsAsync(contractHash);
            }
            public Task<Response<EntryPointCostData>> GetContractEntryPointCostsAsync(string contractHash, string entryPointName)
            {
                return _commonEndpoint.GetContractEntryPointCostsAsync(contractHash, entryPointName);
            }
            public Task<ContractResponse<ContractPackageData>> GetContractPackageAsync(string contractPackageHash)
            {
                return _commonEndpoint.GetContractPackageAsync(contractPackageHash);
            }
            public Task<PaginatedResponse<ContractPackageData>> GetContractPackagesAsync(ContractPackageRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackagesAsync(parameters);
            }
            public Task<PaginatedResponse<ContractPackageData>> GetAccountContractPackagesAsync(string publicKey, AccountContractPackageRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountContractPackagesAsync(publicKey, parameters);
            }
            public Task<PaginatedResponse<DelegationData>> GetAccountDelegationsAsync(string publicKey, DelegationRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountDelegationsAsync(publicKey, parameters);
            }
            public Task<PaginatedResponse<DelegationData>> GetValidatorDelegationsAsync(string publicKey, DelegationRequestParameters parameters = null)
            {
                return _commonEndpoint.GetValidatorDelegationsAsync(publicKey, parameters);
            }
            public Task<PaginatedResponse<DelegatorRewardData>> GetAccountDelegatorRewardsAsync(string publicKey, AccountDelegatorRewardRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountDelegatorRewardsAsync(publicKey, parameters);
            }
            public Task<ulong> GetTotalAccountDelegationRewards(string publicKey)
            {
                return _commonEndpoint.GetTotalAccountDelegationRewards(publicKey);
            }
            public Task<ulong> GetTotalValidatorDelegationRewards(string publicKey)
            {
                return _commonEndpoint.GetTotalValidatorDelegationRewards(publicKey);
            }
            public Task<Response<DeployData>> GetDeployAsync(string deployHash, DeployRequestParameters parameters = null)
            {
                return _commonEndpoint.GetDeployAsync(deployHash, parameters);
            }
            public Task<PaginatedResponse<DeployData>> GetDeploysAsync(DeploysRequestParameters parameters = null)
            {
                return _commonEndpoint.GetDeploysAsync(parameters);
            }
            public Task<PaginatedResponse<DeployData>> GetAccountDeploysAsync(string publicKey, AccountDeploysRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountDeploysAsync(publicKey, parameters);
            }
            public Task<PaginatedResponse<DeployData>> GetBlockDeploysAsync(string blockIdentifier, BlockDeploysRequestParameters parameters = null)
            {
                return _commonEndpoint.GetBlockDeploysAsync(blockIdentifier, parameters);
            }
            public Task<Response<List<DeployExecutionTypesData>>> GetDeployExecutionTypesAsync()
            {
                return _commonEndpoint.GetDeployExecutionTypesAsync();
            }
            public Task<PaginatedResponse<FTTokenActionData>> GetFungibleTokenActionsAsync(FTActionRequestParameters parameters = null)
            {
                return _commonEndpoint.GetFungibleTokenActionsAsync(parameters);
            }
            public Task<PaginatedResponse<FTTokenActionData>> GetAccountFungibleTokenActionsAsync(string accountIdentifier, FTAccountActionRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountFungibleTokenActionsAsync(accountIdentifier, parameters);
            }
            public Task<PaginatedResponse<FTTokenActionData>> GetContractPackageFungibleTokenActionsAsync(string contractPackageHash, FTContractPackageActionRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackageFungibleTokenActionsAsync(contractPackageHash, parameters);
            }
            public Task<PaginatedResponse<FTOwnershipData>> GetAccountFungibleTokenOwnershipAsync(string accountIdentifier, FTAccountOwnershipRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountFungibleTokenOwnershipAsync(accountIdentifier, parameters);
            }
            public Task<PaginatedResponse<FTOwnershipData>> GetContractPackageFungibleTokenOwnershipAsync(string contractPackageHash)
            {
                return _commonEndpoint.GetContractPackageFungibleTokenOwnershipAsync(contractPackageHash);
            }
            public Task<Response<NFTTokenData>> GetNonFungibleTokenAsync(string contractPackageHash, string tokenId, NFTRequestParameters parameters = null)
            {
                return _commonEndpoint.GetNFTAsync(contractPackageHash, tokenId, parameters);
            }
            public Task<PaginatedResponse<NFTTokenData>> GetAccountNFTsAsync(string accountIdentifier, NFTAccountRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountNFTsAsync(accountIdentifier, parameters);
            }
            public Task<PaginatedResponse<NFTTokenData>> GetContractPackageNFTsAsync(string contractPackageHash, NFTContractPackageRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackageNFTsAsync(contractPackageHash, parameters);
            }
            public Task<ListResponse<NFTStandardData>> GetNFTStandardsAsync()
            {
                return _commonEndpoint.GetNFTStandardsAsync();
            }
            public Task<ListResponse<NFTMetadataStatusData>> GetOffchainNFTMetadataStatusesAsync()
            {
                return _commonEndpoint.GetOffchainNFTMetadataStatusesAsync();
            }
            public Task<PaginatedResponse<NFTTokenActionData>> GetContractPackageNFTActionsForATokenAsync(string contractPackageHash, string tokenId, NFTContractPackageTokenActionsRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackageNFTActionsForATokenAsync(contractPackageHash, tokenId, parameters);
            }
            public Task<PaginatedResponse<NFTTokenActionData>> GetAccountNFTActionsAsync(string accountIdentifier, NFTAccountActionsRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountNFTActionsAsync(accountIdentifier, parameters);
            }
            public Task<PaginatedResponse<NFTTokenActionData>> GetContractPackageNFTActionsAsync(string contractPackageHash, NFTContractPackageActionsRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackageNFTActionsAsync(contractPackageHash, parameters);
            }
            public Task<ListResponse<NFTActionTypesData>> GetNFTActionTypesAsync()
            {
                return _commonEndpoint.GetNFTActionTypesAsync();
            }
            public Task<PaginatedResponse<NFTTokenOwnershipData>> GetContractPackageNFTOwnershipAsync(string contractPackageHash, NFTContractPackageOwnershipRequestParameters parameters = null)
            {
                return _commonEndpoint.GetContractPackageNFTOwnershipAsync(contractPackageHash, parameters);
            }
            public Task<PaginatedResponse<NFTTokenOwnershipData>> GetAccountNFTOwnershipAsync(string accountIdentifier, NFTAccountOwnershipRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountNFTOwnershipAsync(accountIdentifier, parameters);
            }
            public Task<Response<RateData>> GetCurrentCurrencyRateAsync(string currencyId)
            {
                return _commonEndpoint.GetCurrentCurrencyRateAsync(currencyId);
            }
            public Task<PaginatedResponse<RateData>> GetHistoricalCurrencyRatesAsync(string currencyId, RateHistoricalRequestParameters parameters = null)
            {
                return _commonEndpoint.GetHistoricalCurrencyRatesAsync(currencyId, parameters);
            }
            public Task<PaginatedResponse<CurrencyData>> GetCurrenciesAsync(RateCurrenciesRequestParameters parameters = null)
            {
                return _commonEndpoint.GetCurrenciesAsync(parameters);
            }
            public Task<Response<SupplyData>> GetSupplyAsync()
            {
                return _commonEndpoint.GetSupplyAsync();
            }

        }
        public class Transfer
        {
            private readonly CommonEndpoint _commonEndpoint;

            public Transfer(CommonEndpoint commonEndpoint)
            {
                _commonEndpoint = commonEndpoint;
            }
            public Task<PaginatedResponse<TransferData>> GetAccountTransfersAsync(string accountIdentifier, TransferAccountRequestParameters parameters = null)
            {
                return _commonEndpoint.GetAccountTransfersAsync(accountIdentifier, parameters);
            }
            public Task<PaginatedResponse<TransferData>> GetDeployTransfersAsync(string deployHash, TransferDeployRequestParameters parameters = null)
            {
                return _commonEndpoint.GetDeployTransfersAsync(deployHash, parameters);
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
        }


    }
}