using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class ProfitComission : Entity<string>
    {
        public decimal TotalDeposit { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal CurrentProfit { get; set; }
        public int TotalReferral { get; set; }
        public int Level { get; set; }
        public IEnumerable<HistoryDeposit> HistoryDeposits { get; set; }
        public IEnumerable<HistoryReceiveProfit> HistoryReceiveProfits { get; set; }
        public IEnumerable<HistoryGetProfitBalance> HistoryGetProfitBalances { get; set; }
    }    
}
