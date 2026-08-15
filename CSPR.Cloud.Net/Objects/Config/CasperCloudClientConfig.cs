using System;

namespace CSPR.Cloud.Net.Objects.Config
{
    public class CasperCloudClientConfig
    {
        public string ApiKey { get; set; }

        /// <summary>
        /// When <c>true</c>, a row inside a response's <c>data</c> array that fails to deserialize is
        /// dropped and counted in <c>SkippedItemCount</c> instead of failing the entire response.
        /// Errors in the envelope itself (<c>item_count</c>, <c>page_count</c>, …) remain fatal —
        /// only rows are tolerated.
        /// <para>Off by default, so behaviour is unchanged unless you opt in. Turn it on when one bad
        /// row must not cost you the whole page — a poller that would otherwise re-fetch, re-fail and
        /// never make progress. Turn it off when silently receiving fewer rows than the API sent is
        /// the worse outcome. Either way, check <c>SkippedItemCount</c> before treating a page as
        /// complete.</para>
        /// </summary>
        public bool TolerateMalformedRows { get; set; }

        public CasperCloudClientConfig(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException("API key is required.", nameof(apiKey));

            ApiKey = apiKey;
        }

    }

}
