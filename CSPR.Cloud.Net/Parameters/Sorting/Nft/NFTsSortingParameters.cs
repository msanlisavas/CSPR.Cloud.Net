using CSPR.Cloud.Net.Parameters.Sorting.Abstract;

namespace CSPR.Cloud.Net.Parameters.Sorting.Nft
{
    /// <summary>
    /// Sorting parameters for the unscoped NFT listing (<c>/nft-tokens</c>). Supports timestamp sorting only.
    /// <para>For more information, see <see href="https://docs.cspr.cloud/documentation/overview/sorting">CSPR.Cloud API documentation</see>.</para>
    /// </summary>
    public class NFTsSortingParameters : TimestampSortingParameters
    {
    }
}
