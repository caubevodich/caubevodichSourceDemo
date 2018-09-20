using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Data;
using Core.Domain.Entities;
using Core.Domain.Models;

namespace Core.Services
{
    public interface IBalanceService : IEntityService<Balance>
    {
        bool CheckTableBalanceInDatabase(string email);
        int InsertBalanceRecord(string email);
        int UpdateBalance(string email, decimal balance);
        Balance GetByEmail(string email);
        decimal GetTotalBalanceByEmail(string email);
    }
    public class BalanceService : EntityService<Balance>, IBalanceService
    {

        public BalanceService(IDbContext context) : base(context)
        {

        }

        public Balance GetByEmail(string email)
        {
            var balance = Table.FirstOrDefault(x => x.UserId == email);
            return balance;
        }

        public bool CheckTableBalanceInDatabase(string email)
        {
            bool check = false;
            var recordExist = this.Find(x => x.UserId == email).SingleOrDefault();
            if (recordExist != null)
            {
                return true;
            }
            return check;
        }

        public int InsertBalanceRecord(string email)
        {
            var bal = new Balance();
            bal.UserId = email;
            bal.TotalBalance = 0;
            bal.CreatedAt = DateTime.UtcNow;
            bal.ModifiedAt = DateTime.UtcNow;
            bal.IsDeleted = false;
            bal.Name = email;
            this.Insert(bal);
            return 1;
        }

        public int UpdateBalance(string email, decimal balance)
        {
            var res = this.Find(x => x.UserId == email).SingleOrDefault();
            if (res != null)
            {
                res.TotalBalance = balance;
                res.ModifiedAt = DateTime.UtcNow;
                this.Update(res);
                return 1;
            }
            return 0;
        }

        public decimal GetTotalBalanceByEmail(string email)
        {
            var res = this.Find(x => x.UserId == email).SingleOrDefault();
            var totalBalance = res.TotalBalance;
            return totalBalance;
        }
    }
}
