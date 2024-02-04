using CSPR.Cloud.Net.Enums;

namespace CSPR.Cloud.Net.Objects.Sorting
{
    public class SortingParameter
    {
        public string FieldName { get; set; }
        public SortType Direction { get; set; }

        public SortingParameter(string fieldName, SortType direction)
        {
            FieldName = fieldName;
            Direction = direction;
        }
    }

}
