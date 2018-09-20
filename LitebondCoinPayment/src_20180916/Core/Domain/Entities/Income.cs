using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{   
    public class Income : Entity<int> /// Int , so Id in DB is primary key - Set identity spec
    {       
        public string UserId { get; set; }
        public DateTime DateReceive { get; set; }
        public decimal TotalAmountIncome { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public IEnumerable<IncomeDetail> IncomeDetails { get; set; }
    }
}
