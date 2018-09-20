namespace Core.Domain.Models
{
    using System.Collections.Generic;
    using Core.Domain.Entities;

    public class WalletModel
    {
        public string Address { get; set; }

        public string Symbol { get; set; }

        public decimal AvailableBalance { get; set; }

        public decimal PendingReceivedBalance { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}