using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class IncomeDetail : Entity<int>
    {
        public string UserId { get; set; }
        public DateTime? DateReceive { get; set; }
        public decimal AmountGet { get; set; }

        /////Use IsDeleted for Status ------------Please ! Remember 
    }
}
