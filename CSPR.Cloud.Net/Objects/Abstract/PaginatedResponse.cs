using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Objects.Abstract
{
    public class PaginatedResponse<T> : ISkipTolerantResponse
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }

        [JsonProperty("item_count")]
        public int ItemCount { get; set; }

        [JsonProperty("page_count")]
        public int PageCount { get; set; }

        /// <summary>
        /// Rows that were present in the wire payload but could not be deserialized, and so are
        /// missing from <see cref="Data"/>. Always <c>0</c> unless
        /// <see cref="Objects.Config.CasperCloudClientConfig.TolerateMalformedRows"/> is enabled —
        /// without it a bad row throws and there is no partial page to report on.
        /// <para>Callers that must not silently lose rows (crediting a ledger from transfers, for
        /// example) should treat a non-zero value as "this page is incomplete" and avoid advancing
        /// any high-water mark past it.</para>
        /// </summary>
        [JsonIgnore]
        public int SkippedItemCount { get; set; }

        int ISkipTolerantResponse.PopulateRows(JArray rows, JsonSerializer serializer)
        {
            Data = new List<T>(rows.Count);
            var skipped = 0;
            foreach (var row in rows)
            {
                try
                {
                    var item = row.ToObject<T>(serializer);
                    if (ReferenceEquals(item, null)) skipped++;
                    else Data.Add(item);
                }
                catch (JsonException)
                {
                    skipped++;
                }
            }
            return skipped;
        }
    }
}
