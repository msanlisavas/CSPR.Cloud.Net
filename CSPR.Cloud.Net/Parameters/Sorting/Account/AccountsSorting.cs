using CSPR.Cloud.Net.Enums;
using System.Collections.Generic;

namespace CSPR.Cloud.Net.Parameters.Sorting.Account
{
    public class AccountsSorting
    {
        public List<string> OrderBy { get; set; } = new List<string>();
        public SortType SortType { get; set; } = SortType.Descending;
    }
}
