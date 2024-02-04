using CSPR.Cloud.Net.Objects.Filter;
using CSPR.Cloud.Net.Objects.Optional;
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
            List<FilteringCriterion> filteringCriteria = null
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

            // Filter out any empty strings to avoid unnecessary ampersands
            var nonEmptyParts = parts.Where(part => !string.IsNullOrWhiteSpace(part));
            return string.Join("&", nonEmptyParts);
        }



        public static string GenerateSortingQueryString(List<SortingParameter> sortingParameters)
        {
            if (sortingParameters == null || !sortingParameters.Any())
                return string.Empty;

            var sortingQueries = sortingParameters.Select(param =>
                $"order_by={param.FieldName}&order_direction={param.Direction.ToString().ToUpper()}");

            return string.Join("&", sortingQueries);
        }
        public static string GenerateIncludesParameter(List<OptionalInclusion> inclusions)
        {
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
