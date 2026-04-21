using System;
using System.Collections.Generic;
using System.Text;

namespace CSPR.Cloud.Net.Clients.Socket
{
    /// <summary>
    /// Builds <c>wss://...</c> URLs for the streaming endpoints. List-valued filters are joined
    /// with commas to match CSPR Cloud's expected format (e.g. <c>?account_hash=a,b,c</c>).
    /// </summary>
    public static class StreamUrlBuilder
    {
        public static Uri Build(string baseUrl, string path, IEnumerable<KeyValuePair<string, IReadOnlyList<string>>> filters)
        {
            if (string.IsNullOrEmpty(baseUrl)) throw new ArgumentException("baseUrl is required", nameof(baseUrl));
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("path is required", nameof(path));

            var sb = new StringBuilder();
            sb.Append(baseUrl);
            sb.Append(path);

            bool first = true;
            if (filters != null)
            {
                foreach (var pair in filters)
                {
                    if (pair.Value == null || pair.Value.Count == 0) continue;
                    sb.Append(first ? '?' : '&');
                    first = false;
                    sb.Append(Uri.EscapeDataString(pair.Key));
                    sb.Append('=');
                    for (int i = 0; i < pair.Value.Count; i++)
                    {
                        if (i > 0) sb.Append(',');
                        sb.Append(Uri.EscapeDataString(pair.Value[i] ?? string.Empty));
                    }
                }
            }

            return new Uri(sb.ToString());
        }

        public static KeyValuePair<string, IReadOnlyList<string>> Filter(string key, IReadOnlyList<string> values)
        {
            return new KeyValuePair<string, IReadOnlyList<string>>(key, values);
        }

        public static KeyValuePair<string, IReadOnlyList<string>> Filter(string key, string value)
        {
            return new KeyValuePair<string, IReadOnlyList<string>>(key, string.IsNullOrEmpty(value) ? null : new[] { value });
        }
    }
}
