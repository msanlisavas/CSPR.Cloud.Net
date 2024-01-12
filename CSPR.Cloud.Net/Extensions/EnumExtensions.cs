using System;
using System.Runtime.Serialization;

namespace CSPR.Cloud.Net.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumMemberValue(this Enum enumValue)
        {
            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());
            var attribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(memberInfo[0], typeof(EnumMemberAttribute));
            return attribute?.Value;
        }


    }
}
