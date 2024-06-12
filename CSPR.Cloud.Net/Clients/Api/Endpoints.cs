using CSPR.Cloud.Net.Helpers;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Account;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Block;
using CSPR.Cloud.Net.Parameters.Wrapper.Accounts;
using CSPR.Cloud.Net.Parameters.Wrapper.Bidder;
using CSPR.Cloud.Net.Parameters.Wrapper.Block;
using CSPR.Cloud.Net.Parameters.Wrapper.CentralizedAccountInfo;
using CSPR.Cloud.Net.Parameters.Wrapper.Contract;
using CSPR.Cloud.Net.Parameters.Wrapper.Delegate;
using CSPR.Cloud.Net.Parameters.Wrapper.Deploy;
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
            public static string GetContractPackages { get; } = "/contract-packages";
            public static string GetAccountContractPackages { get; } = "/accounts/{0}/contract-packages";
            public static string GetAccountDelegations { get; } = "/accounts/{0}/delegations";
            public static string GetValidatorDelegations { get; } = "/validators/{0}/delegations";
            public static string GetAccountDelegatorRewards { get; } = "/accounts/{0}/delegation-rewards";
            public static string GetTotalAccountDelegationRewards { get; } = "/accounts/{0}/total-delegation-rewards";
            public static string GetTotalValidatorDelegatorsRewards { get; } = "/validators/{0}/total-delegator-rewards";
            public static string GetDeploy { get; } = "/deploys/{0}";
            public static string GetDeploys { get; } = "/deploys";
            public static string GetAccountDeploys { get; } = "/accounts/{0}/deploys";
            public static string GetBlockDeploys { get; } = "/blocks/{0}/deploys";
            public static string GetDeployExecutionTypes { get; } = "/deploy-execution-types";
            public static string GetFungibleTokenActions { get; } = "/ft-token-actions";

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
            public static string GetContractPackages(string baseUrl, ContractPackageRequestParameters requestParams = null)
            {
                var url = $"{baseUrl}{BaseUrls.GetContractPackages}";
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
            public static string GetAccountContractPackages(string baseUrl, string publicKey, AccountContractPackageRequestParameters requestParams = null)
            {
                var url = FormatUrlWithParameter(baseUrl, BaseUrls.GetAccountContractPackages, publicKey);
                if (requestParams != null)
                {
                    var sortingParameters = CasperHelpers.CreateSortingParameters(requestParams.SortingParameters);
                    var paginationParameters = CasperHelpers.CreatePaginationParameters(requestParams.PageNumber, requestParams.PageSize);
                    var optionalParameters = CasperHelpers.CreateOptionalParameters(requestParams.OptionalParameters);
                    var queryString = CasperHelpers.BuildQueryString
                        (
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

        }
        public static class Delegate
        {
            public static string GetAccountDelegations(string baseUrl, string publicKey, DelegationRequestParameters requestParams = null)
            {
                var url = FormatUrlWithParameter(baseUrl, BaseUrls.GetAccountDelegations, publicKey);
                if (requestParams != null)
                {
                    var sortingParameters = CasperHelpers.CreateSortingParameters(requestParams.SortingParameters);
                    var paginationParameters = CasperHelpers.CreatePaginationParameters(requestParams.PageNumber, requestParams.PageSize);
                    var optionalParameters = CasperHelpers.CreateOptionalParameters(requestParams.OptionalParameters);
                    var queryString = CasperHelpers.BuildQueryString
                        (
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
            public static string GetValidatorDelegations(string baseUrl, string publicKey, DelegationRequestParameters requestParams = null)
            {
                var url = FormatUrlWithParameter(baseUrl, BaseUrls.GetValidatorDelegations, publicKey);
                if (requestParams != null)
                {
                    var sortingParameters = CasperHelpers.CreateSortingParameters(requestParams.SortingParameters);
                    var paginationParameters = CasperHelpers.CreatePaginationParameters(requestParams.PageNumber, requestParams.PageSize);
                    var optionalParameters = CasperHelpers.CreateOptionalParameters(requestParams.OptionalParameters);
                    var queryString = CasperHelpers.BuildQueryString
                        (
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
            public static string GetAccountDelegatorRewards(string baseUrl, string publicKey, AccountDelegatorRewardRequestParameters requestParams = null)
            {
                var url = FormatUrlWithParameter(baseUrl, BaseUrls.GetAccountDelegatorRewards, publicKey);
                if (requestParams != null)
                {
                    var sortingParameters = CasperHelpers.CreateSortingParameters(requestParams.SortingParameters);
                    var filterParameters = CasperHelpers.CreateFilteringParameters(requestParams.FilterParameters);
                    var paginationParameters = CasperHelpers.CreatePaginationParameters(requestParams.PageNumber, requestParams.PageSize);
                    var optionalParameters = CasperHelpers.CreateOptionalParameters(requestParams.OptionalParameters);
                    var queryString = CasperHelpers.BuildQueryString
                        (
                        sortingParameters: sortingParameters,
                        paginationParameters: paginationParameters,
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
            public static string GetTotalAccountDelegationRewards(string baseUrl, string publicKey)
            {
                var url = FormatUrlWithParameter(baseUrl, BaseUrls.GetTotalAccountDelegationRewards, publicKey);
                return url;
            }
            public static string GetTotalValidatorDelegatorsRewards(string baseUrl, string publicKey)
            {
                var url = FormatUrlWithParameter(baseUrl, BaseUrls.GetTotalValidatorDelegatorsRewards, publicKey);
                return url;
            }
        }
        public static class Deploy
        {
            public static string GetDeploy(string baseUrl, string deployHash, DeployRequestParameters requestParams = null)
            {
                var url = FormatUrlWithParameter(baseUrl, BaseUrls.GetDeploy, deployHash);
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
            public static string GetDeploys(string baseUrl, DeploysRequestParameters requestParams = null)
            {
                var url = $"{baseUrl}{BaseUrls.GetDeploys}";
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
            public static string GetAccountDeploys(string baseUrl, string publicKey, AccountDeploysRequestParameters requestParams = null)
            {
                var url = FormatUrlWithParameter(baseUrl, BaseUrls.GetAccountDeploys, publicKey);
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
            public static string GetBlockDeploys(string baseUrl, string blockIdentifier, BlockDeploysRequestParameters requestParams = null)
            {
                var url = FormatUrlWithParameter(baseUrl, BaseUrls.GetBlockDeploys, blockIdentifier);
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
            public static string GetDeployExecutionTypes(string baseUrl)
            {
                var url = $"{baseUrl}{BaseUrls.GetDeployExecutionTypes}";
                return url;
            }
        }
        public static class FungibleToken
        {

        }
    }
}