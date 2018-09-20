using System.Collections.Generic;
using Core.Domain.Entities;

namespace Core.Domain.Models
{
    public class NetworkIncomeModel
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
