using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class ProfitDailyModel
    {
        public int Id { get; set; }
        public int DepositId { get; set; }
        public string UserId { get; set; }
        public DateTime DateReceive { get; set; }
        public decimal AmountDaily { get; set; }
        public bool Status { get; set; }
        public decimal ProfitOfDeposit { get; set; }
}
}
