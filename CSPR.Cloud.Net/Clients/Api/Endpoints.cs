﻿using CSPR.Cloud.Net.Helpers;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Account;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Block;
using CSPR.Cloud.Net.Parameters.Wrapper.Accounts;
using CSPR.Cloud.Net.Parameters.Wrapper.Bidder;
using CSPR.Cloud.Net.Parameters.Wrapper.Block;
using CSPR.Cloud.Net.Parameters.Wrapper.CentralizedAccountInfo;
using CSPR.Cloud.Net.Parameters.Wrapper.Contract;
using System;

namespace CSPR.Cloud.Net.Clients.Api
{
    public static class Endpoints
    {
        public static class BaseUrls
        {
            public static string Mainnet { get; } = "https://api.mainnet.cspr.cloud";
            public static string Testnet { get; } = "https://api.testnet.cspr.cloud";
            public static string GetAccount { get; } = "/accounts/";
            public static string GetAccounts { get; } = "/accounts";
            public static string GetBlock { get; } = "/blocks/";
            public static string GetBlocks { get; } = "/blocks";
            public static string GetValidatorBlocks { get; } = "/validators/{0}/blocks";
            public static string GetBidder { get; } = "/bidders/";
            public static string GetBidders { get; } = "/bidders";
            public static string GetCentralizedAccountInfo { get; } = "/centralized-account-info/";
            public static string GetCentralizedAccounts { get; } = "/centralized-account-info";
            public static string GetContract { get; } = "/contracts/";
            public static string GetContracts { get; } = "/contracts";
            public static string GetContractsByContractPackage { get; } = "/contract-packages/{0}/contracts";
            public static string GetContractTypes { get; } = "/contract-types";
            public static string GetContractEntryPoints { get; } = "/contracts/{0}/entry-points";
            public static string GetContractEntryPointCosts { get; } = "/contracts/{0}/entry-points/{1}/costs";
            public static string GetContractPackage { get; } = "/contract-packages/";

        }
        public static string FormatUrlWithParameter(string baseUrl, string urlTemplate, params object[] parameters)
        {
            var formattedPath = string.Format(urlTemplate, parameters);
            return $"{baseUrl}{formattedPath}";
        }
        public static class Account
        {
            public static string GetAccount(string baseUrl, string publicKey, AccountsOptionalParameters optParameters = null)
            {
                var url = $"{baseUrl}{BaseUrls.GetAccount}{publicKey}";

                if (optParameters != null)
                {
                    var optionalParameters = CasperHelpers.CreateOptionalParameters(optParameters);
                    var queryString = CasperHelpers.BuildQueryString
                        (
                         optionalParameters: optionalParameters
                        );

                    if (!string.IsNullOrEmpty(queryString))
                    {
                        url = $"{url}?{queryString}";
                    }
                }




                return url;
            }
            public static string GetAccounts(string baseUrl, AccountsRequestParameters requestParams = null)
            {
                var url = $"{baseUrl}{BaseUrls.GetAccounts}";
                if (requestParams != null)
                {
                    var filterParameters = CasperHelpers.CreateFilteringParameters(requestParams.QueryParameters);
                    if (requestParams.Sorting.OrderByBalance && requestParams.Sorting.OrderByTotalBalance)
                    {
                        throw new InvalidOperationException("Both OrderByBalance and OrderByTotalBalance cannot be true at the same time. Choose only one to sort.");
                    }
                    var sortingParameters = CasperHelpers.CreateSortingParameters(requestParams.Sorting);
                    var optionalParameters = CasperHelpers.CreateOptionalParameters(requestParams.OptionalParameters);
                    var paginationParameters = CasperHelpers.CreatePaginationParameters(requestParams.PageNumber, requestParams.PageSize);

                    var queryString = CasperHelpers.BuildQueryString
                        (
                            optionalParameters: optionalParameters,
                            sortingParameters: sortingParameters,
                            filteringCriteria: filterParameters,
                            paginationParameters: paginationParameters
                        );
                    if (!string.IsNullOrEmpty(queryString))
                    {
                        url = $"{url}?{queryString}";
                    }
                }



                return url;
            }



        }
        public static class Block
        {
            public static string GetBlock(string baseUrl, string blockIdentifier, BlockOptionalParameters parameters = null)
            {
                var url = $"{baseUrl}{BaseUrls.GetBlock}{blockIdentifier}";
                if (parameters != null)
                {
                    var optionalParameters = CasperHelpers.CreateOptionalParameters(parameters);
                    var queryString = CasperHelpers.BuildQueryString(optionalParameters: optionalParameters);
                    if (!string.IsNullOrEmpty(queryString))
                    {
                        url = $"{url}?{queryString}";
                    }

                }
                return url;
            }
            public static string GetBlocks(string baseUrl, BlockRequestParameters requestParams = null)
            {
                var url = $"{baseUrl}{BaseUrls.GetBlocks}";

                if (requestParams != null)
                {
                    var filterParameters = CasperHelpers.CreateFilteringParameters(requestParams.FilterParameters);
                    var sortingParameters = CasperHelpers.CreateSortingParameters(requestParams.Sorting);
                    var optionalParameters = CasperHelpers.CreateOptionalParameters(requestParams.OptionalParameters);
                    var paginationParameters = CasperHelpers.CreatePaginationParameters(requestParams.PageNumber, requestParams.PageSize);

                    var queryString = CasperHelpers.BuildQueryString
                        (
                         optionalParameters: optionalParameters,
                         sortingParameters: sortingParameters,
                         filteringCriteria: filterParameters,
                         paginationParameters: paginationParameters
                        );

                    if (!string.IsNullOrEmpty(queryString))
                    {
                        url = $"{url}?{queryString}";
                    }
                }
                return url;
            }
            public static string GetValidatorBlocks(string baseUrl, string validatorPublicKey, BlockRequestParameters requestParams = null)
            {
                var url = FormatUrlWithParameter(baseUrl, BaseUrls.GetValidatorBlocks, validatorPublicKey);
                if (requestParams != null)
                {
                    var sortingParameters = CasperHelpers.CreateSortingParameters(requestParams.Sorting);
                    var optionalParameters = CasperHelpers.CreateOptionalParameters(requestParams.OptionalParameters);
                    var paginationParameters = CasperHelpers.CreatePaginationParameters(requestParams.PageNumber, requestParams.PageSize);

                    var queryString = CasperHelpers.BuildQueryString
                        (
                        optionalParameters: optionalParameters,
                        sortingParameters: sortingParameters,
                        paginationParameters: paginationParameters
                        );

                    if (!string.IsNullOrEmpty(queryString))
                    {
                        url = $"{url}?{queryString}";
                    }
                }
                return url;
            }

        }
        public static class Bidder
        {
            public static string GetBidder(string baseUrl, string publicKey, BidderRequestParameters requestParams)
            {
                var url = $"{baseUrl}{BaseUrls.GetBidder}{publicKey}";
                if (requestParams != null)
                {
                    var optionalParameters = CasperHelpers.CreateOptionalParameters(requestParams.OptionalParameters);
                    var filterParameters = CasperHelpers.CreateFilteringParameters(requestParams.FilterParameters);
                    var queryString = CasperHelpers.BuildQueryString
                        (
                        optionalParameters: optionalParameters,
                        filteringCriteria: filterParameters
                        );
                    if (!string.IsNullOrEmpty(queryString))
                    {
                        url = $"{url}?{queryString}";
                    }
                }
                return url;
            }
            public static string GetBidders(string baseUrl, BiddersRequestParameters requestParams)
            {
                var url = $"{baseUrl}{BaseUrls.GetBidders}";
                if (requestParams != null)
                {
                    var optionalParameters = CasperHelpers.CreateOptionalParameters(requestParams.OptionalParameters);
                    var filterParameters = CasperHelpers.CreateFilteringParameters(requestParams.FilterParameters);
                    var paginationParameters = CasperHelpers.CreatePaginationParameters(requestParams.PageNumber, requestParams.PageSize);
                    var sortingParameters = CasperHelpers.CreateSortingParameters(requestParams.SortingParameters);
                    var queryString = CasperHelpers.BuildQueryString
                        (
                        optionalParameters: optionalParameters,
                        filteringCriteria: filterParameters,
                        paginationParameters: paginationParameters,
                        sortingParameters: sortingParameters
                        );
                    if (!string.IsNullOrEmpty(queryString))
                    {
                        url = $"{url}?{queryString}";
                    }
                }
                return url;
            }
        }
        public static class CentralizedAccountInfo
        {
            public static string GetCentralizedAccountInfo(string baseUrl, string accountHash)
            {
                var url = $"{baseUrl}{BaseUrls.GetCentralizedAccountInfo}{accountHash}";
                return url;
            }
            public static string GetCentralizedInfos(string baseUrl, CentralizedAccountInfoRequestParameters requestParams)
            {
                var url = $"{baseUrl}{BaseUrls.GetCentralizedAccountInfo}";
                if (requestParams != null)
                {
                    var filterParameters = CasperHelpers.CreateFilteringParameters(requestParams.FilterParameters);
                    var sortingParameters = CasperHelpers.CreateSortingParameters(requestParams.SortingParameters);
                    var queryString = CasperHelpers.BuildQueryString
                        (
                        filteringCriteria: filterParameters,
                        sortingParameters: sortingParameters
                        );
                    if (!string.IsNullOrEmpty(queryString))
                    {
                        url = $"{url}?{queryString}";
                    }
                }
                return url;
            }
        }
        public static class Contract
        {
            public static string GetContract(string baseUrl, string contractHash, ContractRequestParameters requestParams = null)
            {
                var url = $"{baseUrl}{BaseUrls.GetContract}{contractHash}";
                if (requestParams != null)
                {
                    var optionalParameters = CasperHelpers.CreateOptionalParameters(requestParams.OptionalParameters);
                    var queryString = CasperHelpers.BuildQueryString
                        (
                        optionalParameters: optionalParameters
                        );
                    if (!string.IsNullOrEmpty(queryString))
                    {
                        url = $"{url}?{queryString}";
                    }
                }
                return url;
            }
            public static string GetContracts(string baseUrl, ContractsRequestParameters requestParams = null)
            {
                var url = $"{baseUrl}{BaseUrls.GetContracts}";
                if (requestParams != null)
                {
                    var filterParameters = CasperHelpers.CreateFilteringParameters(requestParams.FilterParameters);
                    var sortingParameters = CasperHelpers.CreateSortingParameters(requestParams.SortingParameters);
                    var paginationParameters = CasperHelpers.CreatePaginationParameters(requestParams.PageNumber, requestParams.PageSize);
                    var optionalParameters = CasperHelpers.CreateOptionalParameters(requestParams.OptionalParameters);
                    var queryString = CasperHelpers.BuildQueryString
                        (
                        filteringCriteria: filterParameters,
                        sortingParameters: sortingParameters,
                        paginationParameters: paginationParameters,
                        optionalParameters: optionalParameters
                        );
                    if (!string.IsNullOrEmpty(queryString))
                    {
                        url = $"{url}?{queryString}";
                    }
                }
                return url;
            }
            public static string GetContractsByContractPackage(string baseUrl, string contractPackageHash, ByContractRequestParameters requestParams = null)
            {
                var url = FormatUrlWithParameter(baseUrl, BaseUrls.GetContractsByContractPackage, contractPackageHash);
                if (requestParams != null)
                {
                    var filterParameters = CasperHelpers.CreateFilteringParameters(requestParams.FilterParameters);
                    var sortingParameters = CasperHelpers.CreateSortingParameters(requestParams.SortingParameters);
                    var paginationParameters = CasperHelpers.CreatePaginationParameters(requestParams.PageNumber, requestParams.PageSize);
                    var optionalParameters = CasperHelpers.CreateOptionalParameters(requestParams.OptionalParameters);
                    var queryString = CasperHelpers.BuildQueryString
                        (
                        filteringCriteria: filterParameters,
                        sortingParameters: sortingParameters,
                        paginationParameters: paginationParameters,
                        optionalParameters: optionalParameters
                        );
                    if (!string.IsNullOrEmpty(queryString))
                    {
                        url = $"{url}?{queryString}";
                    }
                }
                return url;
            }
            public static string GetContractTypes(string baseUrl)
            {
                var url = $"{baseUrl}{BaseUrls.GetContractTypes}";
                return url;
            }
            public static string GetContractEntryPoints(string baseUrl, string contractHash)
            {
                var url = FormatUrlWithParameter(baseUrl, BaseUrls.GetContractEntryPoints, contractHash);
                return url;
            }
            public static string GetContractEntryPointCosts(string baseUrl, string contractHash, string entryPoint)
            {
                var url = FormatUrlWithParameter(baseUrl, BaseUrls.GetContractEntryPointCosts, contractHash, entryPoint);
                return url;
            }
            public static string GetContractPackage(string baseUrl, string contractPackageHash)
            {
                var url = $"{baseUrl}{BaseUrls.GetContractPackage}{contractPackageHash}";
                return url;
            }
        }
    }
}
