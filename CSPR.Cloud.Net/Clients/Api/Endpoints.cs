using CSPR.Cloud.Net.Enums;
using CSPR.Cloud.Net.Extensions;

namespace CSPR.Cloud.Net.Clients.Api
{
    public static class Endpoints
    {
        public static class BaseUrls
        {
            public static string Mainnet { get; } = "https://api.mainnet.cspr.cloud/";
            public static string Testnet { get; } = "https://api.testnet.cspr.cloud/";
        }
        public static class Account
        {
            public static string GetAccount(string baseUrl, string publicKey, AuctionStatus auctionStatus = AuctionStatus.Empty)
            {
                var url = $"{baseUrl}accounts/{publicKey}";

                if (auctionStatus != AuctionStatus.Empty)
                {
                    url += $"?auction_status={auctionStatus.GetEnumMemberValue()}";
                }
                return url;
            }
        }

    }
}
