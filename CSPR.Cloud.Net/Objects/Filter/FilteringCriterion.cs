using System;
using System.Collections.Generic;
using System.Linq;

namespace CSPR.Cloud.Net.Objects.Filter
{
    public class FilteringCriterion
    {
        public string FieldName { get; set; }
        public List<string> Values { get; set; } = new List<string>();

        public FilteringCriterion(string fieldName)
        {
            FieldName = fieldName;
        }

        public string Serialize()
        {
            if (Values.Count == 1)
            {
                // Single value filter
                return $"{FieldName}={Uri.EscapeDataString(Values[0])}";
            }
            else if (Values.Count > 1)
            {
                // Multiple values filter
                return $"{FieldName}={string.Join(",", Values.Select(Uri.EscapeDataString))}";
            }
            else
            {
                // No values, return an empty string
                return string.Empty;
            }
        }
    }

}
