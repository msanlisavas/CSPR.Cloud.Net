using System.Runtime.Serialization;

namespace CSPR.Cloud.Net.Enums
{
    public enum AuctionStatus
    {
        [EnumMember(Value = "inactive_bidder")]
        InactiveBidder,
        [EnumMember(Value = "active_bidder")]
        ActiveBidder,
        [EnumMember(Value = "active_validator")]
        ActiveValidator,
        [EnumMember(Value = "pending_validator")]
        PendingValidator,
        [EnumMember(Value = "empty")]
        Empty
    }
}
