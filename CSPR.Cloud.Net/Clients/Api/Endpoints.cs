using CSPR.Cloud.Net.Helpers;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Account;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Block;
using CSPR.Cloud.Net.Parameters.Wrapper.Accounts;
using CSPR.Cloud.Net.Parameters.Wrapper.Bidder;
using CSPR.Cloud.Net.Parameters.Wrapper.Block;
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
        }
    }
}
