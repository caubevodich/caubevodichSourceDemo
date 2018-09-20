using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{   
    public class ReceiveNetworkComission : AuditableEntity<int>
    {
        public string UserId { get; set; }
        public DateTime DateReceiveComission { get; set; }
        public decimal AmountRemain { get; set; }
    }
}
