using System.Collections.Generic;
using System.Linq;

namespace CSPR.Cloud.Net.Objects.Optional
{
    public class OptionalInclusion
    {
        public string Name { get; set; }
        public List<string> Fields { get; set; } = new List<string>();
        public List<OptionalInclusion> NestedInclusions { get; set; } = new List<OptionalInclusion>();
        public List<object> FunctionParameters { get; set; } = new List<object>();

        public OptionalInclusion(string name)
        {
            Name = name;
        }

        public string Serialize()
        {
            if (FunctionParameters.Any())
            {
                // Function form: name(param1,param2,...)
                return $"{Name}({string.Join(",", FunctionParameters)})";
            }
            else if (NestedInclusions.Any() || Fields.Any())
            {
                // Object querying syntax: name{field1,field2,nestedObject{...}}
                var allFields = Fields.Concat(NestedInclusions.Select(nested => nested.Serialize()));
                return $"{Name}{{{string.Join(",", allFields)}}}";
            }
            else
            {
                // Scalar value: just the name
                return Name;
            }
        }
    }
}
