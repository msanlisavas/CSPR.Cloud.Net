﻿using CSPR.Cloud.Net.Clients.Api;
using CSPR.Cloud.Net.Errors;
using CSPR.Cloud.Net.Interfaces.Clients;
using CSPR.Cloud.Net.Objects.Abstract;
using CSPR.Cloud.Net.Objects.Account;
using CSPR.Cloud.Net.Objects.Bidder;
using CSPR.Cloud.Net.Objects.Block;
using CSPR.Cloud.Net.Objects.CentralizedAccountInfo;
using CSPR.Cloud.Net.Objects.Config;
using CSPR.Cloud.Net.Objects.Contract;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Account;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Block;
using CSPR.Cloud.Net.Parameters.Wrapper.Accounts;
using CSPR.Cloud.Net.Parameters.Wrapper.Bidder;
using CSPR.Cloud.Net.Parameters.Wrapper.Block;
using CSPR.Cloud.Net.Parameters.Wrapper.CentralizedAccountInfo;
using CSPR.Cloud.Net.Parameters.Wrapper.Contract;
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
            public MainnetEndpoint(CasperCloudRestClient casperCloudRestClient)
            {
                _commonEndpoint = new CommonEndpoint(casperCloudRestClient, Endpoints.BaseUrls.Mainnet);
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

        }
        public class TestnetEndpoint
        {
            private readonly CommonEndpoint _commonEndpoint;

            public TestnetEndpoint(CasperCloudRestClient casperCloudRestClient)
            {
                _commonEndpoint = new CommonEndpoint(casperCloudRestClient, Endpoints.BaseUrls.Testnet);
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
        }


    }
}

