using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Profit : Entity<int> /// Int , so Id in DB is primary key - Set identity spec
    {
        public string UserId { get; set; }
        public DateTime DateReceive { get; set; }
        public decimal TotalAmountProfit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
    }
}
