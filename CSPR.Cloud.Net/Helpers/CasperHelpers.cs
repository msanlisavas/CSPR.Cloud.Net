using System;
using System.Collections.Generic;
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

    }
}
