namespace Core.Domain.Entities
{
    public class Wallet : AuditableEntity<int>
    {
        public string UserId { get; set; }

        public string Address { get; set; }

        public string Symbol { get; set; } = "LTC";

        public string Network { get; set; }

        public decimal? AvailableBalance { get; set; } = 0;

        public decimal? PendingReceivedBalance { get; set; } = 0;

        public bool IsDefault { get; set; }
    }
}