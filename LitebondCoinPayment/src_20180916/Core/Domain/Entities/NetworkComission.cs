using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class NetworkComission : Entity<string>
    {
        public decimal TotalIncomeNetwork { get; set; }
        public decimal TotalNetwork { get; set; }

        public decimal TotalIncomeReferral { get; set; }

        public int TotalReferral { get; set; }

        public int Level { get; set; }
        
        public IEnumerable<HistoryReceiveIncome> HistoryReceiveIncomes { get; set; }
        public IEnumerable<HistoryNetworkIncome> HistoryNetworkIncomes { get; set; }
        public IEnumerable<HistoryGetIncomeBalance> HistoryGetIncomeBalances { get; set; }
    }
}
