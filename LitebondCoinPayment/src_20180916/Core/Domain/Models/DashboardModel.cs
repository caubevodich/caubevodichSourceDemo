namespace Core.Domain.Models
{
    public class DashboardModel
    {
        public WalletModel MyWallet { get; set; }

        public decimal Balance { get; set; }

        public int Level { get; set; }

        public decimal Deposit { get; set; }

        public decimal Income { get; set; }

        public int TotalReferral { get; set; }

        public string ReferralLink { get; set; }
        
    }
}