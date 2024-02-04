using CSPR.Cloud.Net.Extensions;
using CSPR.Cloud.Net.Objects.Filter;
using CSPR.Cloud.Net.Objects.Optional;
using CSPR.Cloud.Net.Objects.Pagination;
using CSPR.Cloud.Net.Objects.Sorting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CSPR.Cloud.Net.Helpers
{
    public static class CasperHelpers
    {
        public static string AppendQueryParameters(string baseUrl, Dictionary<string, string> parameters)
        {
            var url = new StringBuilder(baseUrl);

            foreach (var param in parameters)
            {
                if (string.IsNullOrEmpty(param.Value))
                {
                    continue;
                }

                url.Append(url.ToString().Contains("?") ? "&" : "?");
                url.Append($"{param.Key}={Uri.EscapeDataString(param.Value)}");
            }

            return url.ToString();
        }
        public static string BuildQueryString
            (
            List<OptionalInclusion> optionalParameters = null,
            List<SortingParameter> sortingParameters = null,
            List<FilteringCriterion> filteringCriteria = null,
            PaginationParameters paginationParameters = null
            )
        {
            var parts = new List<string>();

            // Check and generate sorting query string if sortingParameters is not null
            if (sortingParameters != null)
            {
                var sortingQueryString = GenerateSortingQueryString(sortingParameters);
                if (!string.IsNullOrWhiteSpace(sortingQueryString))
                {
                    parts.Add(sortingQueryString);
                }
            }

            // Check and generate optional parameters query string if optionalParameters is not null
            if (optionalParameters != null)
            {
                var optionalParametersString = GenerateIncludesParameter(optionalParameters);
                if (!string.IsNullOrWhiteSpace(optionalParametersString))
                {
                    parts.Add(optionalParametersString);
                }
            }

            // Check and generate filtering query string if filteringCriteria is not null
            if (filteringCriteria != null)
            {
                var filteringQueryString = GenerateFilteringParameter(filteringCriteria);
                if (!string.IsNullOrWhiteSpace(filteringQueryString))
                {
                    parts.Add(filteringQueryString);
                }
            }

            // Check and generate pagination query string if paginationParameters is not null
            if (paginationParameters != null)
            {
                var paginationQueryString = GeneratePaginationQueryString(paginationParameters);
                if (!string.IsNullOrWhiteSpace(paginationQueryString))
                {
                    parts.Add(paginationQueryString);
                }
            }

            // Filter out any empty strings to avoid unnecessary ampersands
            var nonEmptyParts = parts.Where(part => !string.IsNullOrWhiteSpace(part));
            return string.Join("&", nonEmptyParts);
        }

        public static string GeneratePaginationQueryString(PaginationParameters paginationParameters)
        {
            if (paginationParameters == null)
            {
                throw new ArgumentNullException(nameof(paginationParameters));
            }

            var queryString = $"page={paginationParameters.Page}&page_size={paginationParameters.PageSize}";

            return queryString;
        }



        public static string GenerateSortingQueryString(List<SortingParameter> sortingParameters)
        {
            if (sortingParameters == null || !sortingParameters.Any())
                return string.Empty;

            var sortingQueries = sortingParameters.Select(param =>
                $"order_by={param.FieldName}&order_direction={param.Direction.GetEnumMemberValue()}");

            return string.Join("&", sortingQueries);
        }
        public static string GenerateIncludesParameter(List<OptionalInclusion> inclusions)
        {
            if (inclusions == null || inclusions.Count == 0)
            {
                return string.Empty; // Return an empty string if there are no inclusions
            }

            return $"includes={string.Join(",", inclusions.Select(inclusion => inclusion.Serialize()))}";
        }

        public static string GenerateFilteringParameter(List<FilteringCriterion> filteringCriteria)
        {
            var filteredCriteria = filteringCriteria.Where(criteria => criteria.Values.Count > 0);
            return string.Join("&", filteredCriteria.Select(criteria => criteria.Serialize()));
        }

        public static List<OptionalInclusion> CreateOptionalParameters<T>(T parameters)
        {
            var optionalParams = new List<OptionalInclusion>();

            foreach (var prop in typeof(T).GetProperties())
            {
                var jsonAttribute = prop.GetCustomAttribute<JsonPropertyAttribute>();
                if (jsonAttribute != null)
                {
                    string propertyName = jsonAttribute.PropertyName;
                    object propValue = prop.GetValue(parameters);

                    // Exclude boolean properties with a value of false
                    if (propValue is bool boolValue && !boolValue)
                    {
                        continue;
                    }

                    // Create an OptionalInclusion object with the JSON property name
                    var optionalInclusion = new OptionalInclusion(propertyName);


                    optionalParams.Add(optionalInclusion);
                }
            }

            return optionalParams;
        }
        public static List<FilteringCriterion> CreateFilteringParameters<T>(T parameters)
        {
            var filteringParams = new List<FilteringCriterion>();

            foreach (var prop in typeof(T).GetProperties())
            {
                var jsonAttribute = prop.GetCustomAttribute<JsonPropertyAttribute>();
                if (jsonAttribute != null)
                {
                    string fieldName = jsonAttribute.PropertyName;
                    var propValue = prop.GetValue(parameters);

                    // Convert the property value to a list of strings
                    if (propValue is IEnumerable<string> values)
                    {
                        var filteringCriterion = new FilteringCriterion(fieldName);
                        filteringCriterion.Values.AddRange(values);
                        filteringParams.Add(filteringCriterion);
                    }
                }
            }

            return filteringParams;
        }
        public static List<SortingParameter> CreateSortingParameters<T>(T parameters)
        {
            var sortingParams = new List<SortingParameter>();

            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.PropertyType == typeof(bool))
                {
                    bool includeProperty = (bool)prop.GetValue(parameters);
                    if (includeProperty)
                    {
                        var jsonAttribute = prop.GetCustomAttribute<JsonPropertyAttribute>();
                        if (jsonAttribute != null)
                        {
                            string fieldName = jsonAttribute.PropertyName;
                            sortingParams.Add(new SortingParameter(fieldName, Enums.SortType.Ascending)); // You can set the direction here
                        }
                    }
                }
            }

            return sortingParams;
        }

        public static PaginationParameters CreatePaginationParameters(int page, int pageSize)
        {
            if (page < 1)
            {
                throw new ArgumentException("Page number should be greater than or equal to 1.", nameof(page));
            }

            if (pageSize < 1 || pageSize > 250)
            {
                throw new ArgumentException("Page size should be between 1 and 250.", nameof(pageSize));
            }

            return new PaginationParameters
            {
                Page = page,
                PageSize = pageSize
            };
        }




        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(params Dictionary<TKey, TValue>[] dictionaries)
        {
            var result = new Dictionary<TKey, TValue>();

            foreach (var dictionary in dictionaries)
            {
                foreach (var kvp in dictionary)
                {
                    result[kvp.Key] = kvp.Value;
                }
            }

            return result;
        }

    }

}
