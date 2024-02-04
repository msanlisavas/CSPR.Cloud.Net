namespace CSPR.Cloud.Net.Parameters.OptionalParameters.Account
{
    public class AccountsOptionalParameters
    {
        public bool AuctionStatus { get; set; } = false;
        public bool DelegatedBalance { get; set; } = false;
        public bool UndelegatingBalance { get; set; } = false;
        public bool StakedBalance { get; set; } = false;
    }
}
