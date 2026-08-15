using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CSPR.Cloud.Net.Objects.Abstract
{
    /// <summary>
    /// Implemented by response envelopes carrying a <c>data</c> array whose rows can be deserialized
    /// one at a time, so a single unusable row costs only itself rather than the whole response, and
    /// the loss is reported rather than silent. Used by
    /// <see cref="Clients.CasperCloudRestClient"/> when
    /// <see cref="Objects.Config.CasperCloudClientConfig.TolerateMalformedRows"/> is enabled.
    /// <para>Deliberately internal: <c>SkippedItemCount</c> is a normal public property on each
    /// envelope, while the mechanics below stay off the public surface.</para>
    /// </summary>
    internal interface ISkipTolerantResponse
    {
        /// <summary>Rows present in the wire payload but absent from <c>Data</c>.</summary>
        int SkippedItemCount { get; set; }

        /// <summary>
        /// Replaces <c>Data</c> by deserializing <paramref name="rows"/> individually, skipping any
        /// row that fails, and returns how many were skipped.
        /// <para>Per-row deserialization rather than a serializer error handler on the whole page:
        /// handling an error at member level tells Newtonsoft to leave that member at its default and
        /// <em>keep</em> the row, which would hand callers a half-populated object and report nothing
        /// skipped — the opposite of the intent.</para>
        /// </summary>
        int PopulateRows(JArray rows, JsonSerializer serializer);
    }
}
