using Core.Data;
using Core.Domain.Entities;
using System;

namespace Core.Services
{
    public interface IHistoryGetIncomeBalanceService : IEntityService<HistoryGetIncomeBalance>
    {
        int InsertHistoryGetIncomeBalance(string email, decimal amount);
    }
    public class HistoryGetIncomeBalanceService : EntityService<HistoryGetIncomeBalance>, IHistoryGetIncomeBalanceService
    {
        public HistoryGetIncomeBalanceService(IDbContext context) : base(context)
        {
        }

        public int InsertHistoryGetIncomeBalance(string email, decimal amount)
        {
            var his = new HistoryGetIncomeBalance();
            his.UserId = email;
            his.DateGetBalance = DateTime.UtcNow;
            his.Amount = amount;
            his.CreatedAt = DateTime.UtcNow;
            his.ModifiedAt = DateTime.UtcNow;
            his.IsDeleted = false;
            his.Name = email;
            this.Insert(his);
            return 1;
        }


    }

}



