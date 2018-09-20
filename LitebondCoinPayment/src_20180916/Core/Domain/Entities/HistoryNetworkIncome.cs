using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class HistoryNetworkIncome : AuditableEntity<int>
    {
        public string UserId { get; set; }
        public DateTime DateReceive { get; set; }
        public decimal Amount { get; set; }  // This is an amount which be caculated by network rules  
        public bool Status { get; set; }
    }
}
