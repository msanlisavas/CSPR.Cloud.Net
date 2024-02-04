using CSPR.Cloud.Net.Extensions;
using CSPR.Cloud.Net.Helpers;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Account;
using CSPR.Cloud.Net.Parameters.Wrapper.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSPR.Cloud.Net.Clients.Api
{
    public static class Endpoints
    {
        public static class BaseUrls
        {
            public static string Mainnet { get; } = "https://api.mainnet.cspr.cloud/";
            public static string Testnet { get; } = "https://api.testnet.cspr.cloud/";
        }
        public static class Account
        {
            public static string GetAccount(string baseUrl, string publicKey, AccountsOptionalParameters optParameters = null)
            {
                var url = $"{baseUrl}accounts/{publicKey}";

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
            public static string GetAccounts(string baseUrl, AccountsRequestParameters requestParams)
            {
                var parameters = new Dictionary<string, string>();

                // Handle Account Hashes
                if (requestParams.QueryParameters?.AccountHashes != null && requestParams.QueryParameters.AccountHashes.Any())
                {
                    string hashes = string.Join(",", requestParams.QueryParameters.AccountHashes);
                    parameters.Add("account_hash", hashes);
                }

                // Handle Sorting
                if (requestParams.Sorting != null)
                {
                    // Validate OrderBy fields
                    var validSortingFields = new HashSet<string> { "balance", "total_balance" };
                    foreach (var field in requestParams.Sorting.OrderBy)
                    {
                        if (!validSortingFields.Contains(field))
                        {
                            throw new ArgumentException($"Invalid sorting field: {field}. Only 'balance' and 'total_balance' are allowed.");
                        }
                    }

                    if (requestParams.Sorting.OrderBy.Any())
                    {
                        string orderByString = requestParams.Sorting.OrderBy.Any()
                                               ? string.Join(",", requestParams.Sorting.OrderBy)
                                               : "total_balance";
                        parameters.Add("order_by", orderByString);
                        parameters.Add("order_direction", requestParams.Sorting.SortType.GetEnumMemberValue());
                    }
                }

                // Handle Optional Parameters
                //var optParameters = CasperHelpers.AppendOptionalQueryParameters(requestParams.OptionalParameters);

                //parameters = CasperHelpers.Merge(parameters, optParameters);

                return CasperHelpers.AppendQueryParameters(baseUrl + "accounts", parameters);
            }



        }

    }
}
