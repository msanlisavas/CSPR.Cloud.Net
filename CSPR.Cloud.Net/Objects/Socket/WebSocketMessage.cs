using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CSPR.Cloud.Net.Objects.Socket
{
    /// <summary>
    /// Envelope for every CSPR Cloud streaming message.
    /// The shape is identical across all streams: action + data + optional extra + timestamp.
    /// For more information, see <see href="https://docs.cspr.cloud/streaming-api/reference">CSPR Cloud Streaming API documentation</see>.
    /// </summary>
    /// <typeparam name="T">The entity type carried in the <c>data</c> field.</typeparam>
    public class WebSocketMessage<T>
    {
        /// <summary>
        /// Operation type. Examples: "created", "updated", "emitted".
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }

        /// <summary>
        /// The modified entity.
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }

        /// <summary>
        /// Additional contextual information. Shape varies by stream (e.g. deploy_hash, owner_hash, block_height).
        /// Kept as <see cref="JObject"/> so callers can inspect fields without tying the SDK to a specific per-stream type.
        /// </summary>
        [JsonProperty("extra")]
        public JObject Extra { get; set; }

        /// <summary>
        /// Time when the triggering network object or block occurred.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }
    }
}
