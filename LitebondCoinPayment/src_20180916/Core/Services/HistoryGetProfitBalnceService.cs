using Core.Data;
using Core.Domain.Entities;
using System;

namespace Core.Services
{
    public interface IHistoryGetProfitBalanceService : IEntityService<HistoryGetProfitBalance>
    {
        int InserHistoryGetProfitBalance(string email, decimal amount);
    }
    public class HistoryGetProfitBalanceService : EntityService<HistoryGetProfitBalance>, IHistoryGetProfitBalanceService
    {
        public HistoryGetProfitBalanceService(IDbContext context) : base(context)
        {
        }

        public int InserHistoryGetProfitBalance(string email, decimal amount)
        {
            var his = new HistoryGetProfitBalance();
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
