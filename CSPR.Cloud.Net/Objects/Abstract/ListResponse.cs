using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Objects.Abstract
{
    public class ListResponse<T> : ISkipTolerantResponse
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }

        /// <summary>
        /// Rows that were present in the wire payload but could not be deserialized, and so are
        /// missing from <see cref="Data"/>. Always <c>0</c> unless
        /// <see cref="Objects.Config.CasperCloudClientConfig.TolerateMalformedRows"/> is enabled.
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
