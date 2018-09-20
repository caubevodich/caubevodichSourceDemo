using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Codition : AuditableEntity<int>
    {
        public int Level { get; set; }
        public double PercentProfitDaily { get; set; }
        public int  Day { get; set; }
        public double PercentProfitAndCapital { get; set; }
        public decimal MinDeposit { get; set; }
        public decimal MaxDeposit { get; set; }
        public int Condition { get; set; }
    }
}
