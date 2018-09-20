using System;

namespace Core.Domain.Entities
{
    public class HistoryDeposit: AuditableEntity<int>
    {
        public string UserId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal Amount { get; set; }
        public int TotalDay { get; set; }
        public bool Status { get; set; }     
        public double PercentProfitDaily { get; set; }
    }
}
