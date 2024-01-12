using System.Runtime.Serialization;

namespace CSPR.Cloud.Net.Enums
{
    public enum SortType
    {
        [EnumMember(Value = "ASC")]
        Ascending,
        [EnumMember(Value = "DESC")]
        Descending
    }
}
