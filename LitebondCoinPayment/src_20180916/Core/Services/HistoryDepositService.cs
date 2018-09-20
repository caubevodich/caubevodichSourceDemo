using Core.Data;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public interface IHistoryDepositService : IEntityService<HistoryDeposit>
    {
        int InsertRecordDepositToIncomeDatabase(string email, decimal amount, int totalDay, double percent);
        List<HistoryDeposit> ListHistoryDeposit(string email);
        decimal calTotalDeposit(string email);
        decimal calTotalDepositWithDate(string email, DateTime date);
        int updateStatusEndDep(string email, int id);
    }
    public class HistoryDepositService : EntityService<HistoryDeposit>, IHistoryDepositService
    {
        public HistoryDepositService(IDbContext context) : base(context)
        {
        }

        public int InsertRecordDepositToIncomeDatabase(string email, decimal amount,int totalDay, double percent)
        {            
            HistoryDeposit deposit = new HistoryDeposit();
            TimeSpan addDay = new TimeSpan(totalDay, 0, 0, 0);
            deposit.UserId = email;
            deposit.DateCreate = DateTime.UtcNow;
            deposit.DateEnd = DateTime.UtcNow.Add(addDay);
            deposit.Amount = amount;
            deposit.TotalDay = totalDay;
            deposit.PercentProfitDaily = percent;
            deposit.CreatedAt = DateTime.UtcNow;
            deposit.ModifiedAt = DateTime.UtcNow;
            deposit.Name = email;
            deposit.IsDeleted = false;
            deposit.Status = true;
            this.Insert(deposit);
            return 1;
        }

        public List<HistoryDeposit> ListHistoryDeposit(string email)
        {
            return this.Find(x => x.UserId == email && x.Status == true && x.DateEnd >= DateTime.UtcNow).ToList();
        }
        public List<HistoryDeposit> ListHistoryDepositWithDate(string email, DateTime dateGetNetworkComssion)
        {
            return this.Find(x => x.UserId == email && x.Status == true && x.DateCreate > dateGetNetworkComssion).ToList();
        }
        public decimal calTotalDeposit(string email)
        {
            decimal total = 0;
            var listDep = ListHistoryDeposit(email);
            foreach (var item in listDep)
            {
                total += item.Amount;
            }
            return total;
        }

        public decimal calTotalDepositWithDate(string email, DateTime dateGetNetworkComssion)
        {
            decimal total = 0;
            var listDep = ListHistoryDepositWithDate(email, dateGetNetworkComssion);
            foreach (var item in listDep)
            {
                total += item.Amount;
            }
            return total;

        }

        public int updateStatusEndDep(string email, int id)
        {
            var record = this.Find(x => x.UserId == email && x.Id == id).SingleOrDefault();
            record.Status = false;
            this.Update(record);
            return 1;
        }

    }
}
