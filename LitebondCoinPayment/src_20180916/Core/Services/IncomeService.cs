using Core.Data;
using Core.Domain.Entities;
using System.Linq;

namespace Core.Services
{
    public interface IIncomeService : IEntityService<Income>
    {
        bool CheckTableIncomeInDatabase(string email);
        int UpdateTotalAmountIncome(string email, decimal extraAmount);
        int InsertRecordToIncomeDatabase(string email, Income income);
        int UpdateTotalAmountIncomeToZero(string email);
    }
    public class IncomeService : EntityService<Income>, IIncomeService
    {
        public IncomeService(IDbContext context) : base(context)
        {
        }

        public bool CheckTableIncomeInDatabase(string email)
        {
            bool check = false;
            var recordExist = this.Find(x => x.UserId == email).FirstOrDefault();
            if (recordExist != null)
            {
                return true;
            }
            return check;
        }

        public int UpdateTotalAmountIncome(string email, decimal extraAmount)
        {
            var income = this.Find(x => x.UserId == email).FirstOrDefault();
            income.TotalAmountIncome = extraAmount + income.TotalAmountIncome;
            income.ModifiedAt = System.DateTime.UtcNow;
            this.Update(income);
            return 1;
        }

        public int InsertRecordToIncomeDatabase(string email, Income income)
        {
            int result = 0;
            if (!CheckTableIncomeInDatabase(email))
            {
                income.UserId = email;
                income.DateReceive = System.DateTime.UtcNow;
                income.TotalAmountIncome = 0;
                income.CreatedAt = System.DateTime.UtcNow;
                income.ModifiedAt = System.DateTime.UtcNow;
                income.Name = email;
                income.IsDeleted = false;
                this.Insert(income);
                return 1;
            }
            return result;
        }

        public int UpdateTotalAmountIncomeToZero(string email)
        {
            var income = this.Find(x => x.UserId == email).FirstOrDefault();
            income.TotalAmountIncome = 0;
            income.ModifiedAt = System.DateTime.UtcNow;
            this.Update(income);
            return 1;
        }
    }
}
