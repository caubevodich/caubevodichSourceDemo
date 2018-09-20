using Core.Data;
using Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public interface IProfitDailyService : IEntityService<ProfitDaily>
    {
        List<ProfitDaily> ListProfitDaily(string email, int depositId);
        int UpdateStatusInProfitDaily(int id, bool status);
        ProfitDaily GetProfitDailyById(int id);
        int InsertRecordProfitDaily(int depositId, string email, decimal amountDaily, System.DateTime subday);
    }
    public class ProfitDailyService : EntityService<ProfitDaily>, IProfitDailyService
    {
        public ProfitDailyService(IDbContext context) : base(context)
        {
        }

        public ProfitDaily GetProfitDailyById(int id)
        {
            var profitDailyRecord = this.Find(x => x.Id == id).FirstOrDefault();
            if (profitDailyRecord == null) profitDailyRecord = new ProfitDaily();
            return profitDailyRecord;
        }
        public List<ProfitDaily> ListProfitDaily(string email,int depositId)
        {
            return this.Find(x => x.UserId == email && x.DepositId == depositId).ToList();
        }

        public int UpdateStatusInProfitDaily (int id, bool status)
        {
            var dailyprofit = this.Get(id);
            dailyprofit.Status = status;
            dailyprofit.ModifiedAt = System.DateTime.UtcNow;
            this.Update(dailyprofit);
            return 1;
        }

        public int InsertRecordProfitDaily(int depositId, string email, decimal amountDaily, System.DateTime subday)
        {
            ProfitDaily profitDaily = new ProfitDaily();
            profitDaily.DepositId = depositId;
            profitDaily.UserId = email;
            profitDaily.DateReceive = subday;
            profitDaily.AmountDaily = amountDaily;
            profitDaily.Status = false;
            profitDaily.Name = email;
            profitDaily.CreatedAt= System.DateTime.UtcNow;
            profitDaily.ModifiedAt = System.DateTime.UtcNow;
            profitDaily.IsDeleted = false;
            this.Insert(profitDaily);
            return 1;
        }

       
    }
}
