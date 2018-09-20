using System;

namespace Core.Domain.Entities
{
    public class Transaction : AuditableEntity<int>
    {
        public string UserId { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string TxId { get; set; }

        public decimal Amount { get; set; }

        public bool Type { get; set; }        

        public string Status { get; set; }

        public int TotalConfirm { get; set; }

        public string Network { get; set; }

        public decimal NetworkFee { get; set; }

        public decimal ExchangeFee { get; set; }
    }
}