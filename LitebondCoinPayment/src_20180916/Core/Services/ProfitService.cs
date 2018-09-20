using Core.Data;
using Core.Domain.Entities;
using System.Linq;

namespace Core.Services
{
    public interface IProfitService : IEntityService<Profit>
    {
        bool CheckTableProfitInDatabase(string email);
        int UpdateTotalAmountProfit(string email, decimal extraAmount);
        int InsertRecordToProfitDatabase(string email, Profit profit);
        int UpdateTotalProfitIncomeToZero(string email);
    }
    public class ProfitService : EntityService<Profit>, IProfitService
    {
        public ProfitService(IDbContext context) : base(context)
        {
        }


        public bool CheckTableProfitInDatabase(string email)
        {
            bool check = false;
            var recordExist = this.Find(x => x.UserId == email).FirstOrDefault();
            if (recordExist != null)
            {
                return true;
            }
            return check;
        }

        public int UpdateTotalAmountProfit(string email, decimal extraAmount)
        {
            var profit = this.Find(x => x.UserId == email).SingleOrDefault();
            profit.TotalAmountProfit = extraAmount + profit.TotalAmountProfit;
            profit.ModifiedAt = System.DateTime.UtcNow;
            this.Update(profit);
            return 1;
        }
        public int InsertRecordToProfitDatabase(string email, Profit profit)
        {
            int result = 0;
            if (!CheckTableProfitInDatabase(email))
            {
                profit.UserId = email;
                profit.DateReceive = System.DateTime.UtcNow;
                profit.TotalAmountProfit = 0;
                profit.CreatedAt = System.DateTime.UtcNow;
                profit.ModifiedAt = System.DateTime.UtcNow;
                profit.Name = email;
                profit.IsDeleted = false;
                this.Insert(profit);
                return 1;
            }
            return result;
        }

        public int UpdateTotalProfitIncomeToZero(string email)
        {
            var profit = this.Find(x => x.UserId == email).FirstOrDefault();
            profit.TotalAmountProfit = 0;
            profit.ModifiedAt = System.DateTime.UtcNow;
            this.Update(profit);
            return 1;
        }


    }
}
