using System;

namespace Core.Domain.Entities
{
    public class HistoryReceiveIncome : AuditableEntity<int>
    {
        public string UserId { get; set; }
        public DateTime DateReceive { get; set; }
        public decimal Amount { get; set; } //Amout of 10% which the downline invest
        public string Referral { get; set; } // Who invest
        public bool Status { get; set; }

    }
}
