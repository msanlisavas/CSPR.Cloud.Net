using System.Runtime.Serialization;

namespace CSPR.Cloud.Net.Enums
{
    /// <summary>
    /// Represents the sort order type for sorting operations.
    /// </summary>
    public enum SortType
    {
        /// <summary>
        /// Sort in ascending order.
        /// </summary>
        [EnumMember(Value = "ASC")]
        Ascending,

        /// <summary>
        /// Sort in descending order.
        /// </summary>
        [EnumMember(Value = "DESC")]
        Descending
    }

}
