using System.Collections.Generic;
using Core.Domain.Entities;

namespace Core.Domain.Models
{
    public class ProfitIncomeModel
    {
        public decimal TotalDeposit { get; set; }
        public decimal TotalProfit { get; set; }

        public decimal CurrentProfit { get; set; }

        public int TotalReferral { get; set; }

        public int Level { get; set; }

        public IEnumerable<HistoryDepositModel> HistoryDepositModels { get; set; }
        //public IEnumerable<HistoryDeposit> HistoryDeposits { get; set; }
        public IEnumerable<ProfitDailyModel> HistoryReceiveProfits { get; set; }
        public IEnumerable<HistoryGetProfitBalance> HistoryGetProfitBalances { get; set; }

    }
}
