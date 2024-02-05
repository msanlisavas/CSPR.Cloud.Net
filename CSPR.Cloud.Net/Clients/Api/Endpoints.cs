using CSPR.Cloud.Net.Helpers;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Account;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Block;
using CSPR.Cloud.Net.Parameters.Wrapper.Accounts;
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
        }
        public static class Account
        {
            public static string GetAccount(string baseUrl, string publicKey, AccountsOptionalParameters optParameters = null)
            {
                var url = $"{baseUrl}{BaseUrls.GetAccount}{publicKey}";

                if (optParameters != null)
                {
                    var optionalParameters = CasperHelpers.CreateOptionalParameters(optParameters);
                    // Assuming BuildQueryString is adapted to handle nulls or empty dictionaries
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

        }
    }
}
