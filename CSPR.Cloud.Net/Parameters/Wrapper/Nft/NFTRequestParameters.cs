using CSPR.Cloud.Net.Parameters.General;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Nft;

namespace CSPR.Cloud.Net.Parameters.Wrapper.Nft
{
    public class NFTRequestParameters : Paginated
    {
        public NFTOptionalParameters OptionalParameters { get; set; }
        public NFTRequestParameters()
        {
            OptionalParameters = new NFTOptionalParameters();

        }
    }
}
