using System.Runtime.Serialization;

namespace CSPR.Cloud.Net.Enums
{
    /// <summary>
    /// Represents the auction status of a bidder or validator in the Casper Network.
    /// </summary>
    public enum AuctionStatus
    {
        /// <summary>
        /// The entity is an inactive bidder.
        /// </summary>
        [EnumMember(Value = "inactive_bidder")]
        InactiveBidder,

        /// <summary>
        /// The entity is an active bidder.
        /// </summary>
        [EnumMember(Value = "active_bidder")]
        ActiveBidder,

        /// <summary>
        /// The entity is an active validator.
        /// </summary>
        [EnumMember(Value = "active_validator")]
        ActiveValidator,

        /// <summary>
        /// The entity is a pending validator.
        /// </summary>
        [EnumMember(Value = "pending_validator")]
        PendingValidator
    }

}
