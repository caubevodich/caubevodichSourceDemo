using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class RefModel
    {
        public string refLink { get; set; }
        public IEnumerable<ReferralModel> RefList { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalRefOfNetwork { get; set; }
    }
}
