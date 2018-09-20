using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Balance : AuditableEntity<int>
    {
        public string UserId { get; set; }        
        public decimal TotalBalance { get; set; }
    }
}
