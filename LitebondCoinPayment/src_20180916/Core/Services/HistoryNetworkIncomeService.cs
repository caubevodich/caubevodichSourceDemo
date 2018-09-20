using Core.Data;
using Core.Domain.Entities;
using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{

    public interface IHistoryNetworkIncomeService : IEntityService<HistoryNetworkIncome>
    {
        HistoryNetworkIncome GetHistoryNetworkById(int id);
        void UpdateStatusInHistoryNetworkIncome(int id, bool status);
        int InsertHistoryNetworkIncome(string email, decimal amount);

    }
    public class HistoryNetworkIncomeService : EntityService<HistoryNetworkIncome>, IHistoryNetworkIncomeService
    {
        private IHistoryDepositService _historyDepositService;
        public HistoryNetworkIncomeService(IDbContext context, IHistoryDepositService historyDepositService) : base(context)
        {
            _historyDepositService = historyDepositService;
        }

        public HistoryNetworkIncome GetHistoryNetworkById(int id)
        {
            var historyRecord = this.Find(x => x.Id == id).FirstOrDefault();
            if (historyRecord == null) historyRecord = new HistoryNetworkIncome();
            return historyRecord;
        }
        public void UpdateStatusInHistoryNetworkIncome(int id, bool status)
        {
            var historyNetworkIncome = this.Get(id);
            historyNetworkIncome.Status = status;
            historyNetworkIncome.ModifiedAt = System.DateTime.UtcNow;
            this.Update(historyNetworkIncome);
        }



        public int InsertHistoryNetworkIncome(string email, decimal amount)
        {
            var his = new HistoryNetworkIncome();
            his.UserId = email;
            his.DateReceive = System.DateTime.UtcNow;
            his.Amount = amount;
            his.CreatedAt = System.DateTime.UtcNow;
            his.ModifiedAt = System.DateTime.UtcNow;
            his.IsDeleted = false;
            his.Name = email;
            his.Status = false;
            this.Insert(his);
            return 1;
        }
    }
}


