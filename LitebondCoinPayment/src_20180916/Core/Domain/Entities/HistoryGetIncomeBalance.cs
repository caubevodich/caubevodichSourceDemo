using System;

namespace Core.Domain.Entities
{
    public class HistoryGetIncomeBalance : AuditableEntity<int>
    {
        public string UserId { get; set; }
        public DateTime DateGetBalance { get; set; }
        public decimal Amount { get; set; }        
    }
}
