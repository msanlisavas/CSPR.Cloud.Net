using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.Sorting.Rate;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Rate
{
    public class RateCurrenciesRequestParameters : Paginated
    {
        public RateCurrenciesSortingParameters SortingParameters { get; set; }
        public RateCurrenciesRequestParameters()
        {
            SortingParameters = new RateCurrenciesSortingParameters();
        }
    }
}
