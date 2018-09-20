using Core.Data;
using Core.Domain.Entities;
using System.Linq;

namespace Core.Services
{
    public interface IHistoryReceiveIncomeService : IEntityService<HistoryReceiveIncome>
    {
        HistoryReceiveIncome GetHistoryReceiveIncomeReferralById(int id);
        int UpdateStatusInHistoryReceiveIncomeReferral(int id, bool status);
        int Insert10PercentForUpline(string parentId, decimal amountReceive, string email);
    }
    public class HistoryReceiveIncomeService : EntityService<HistoryReceiveIncome>, IHistoryReceiveIncomeService
    {
        public HistoryReceiveIncomeService(IDbContext context) : base(context)
        {
        }
        public HistoryReceiveIncome GetHistoryReceiveIncomeReferralById(int id)
        {
            var historyRecord = this.Find(x => x.Id == id).FirstOrDefault();
            if (historyRecord == null) historyRecord = new HistoryReceiveIncome();
            return historyRecord;
        }
        public int UpdateStatusInHistoryReceiveIncomeReferral(int id, bool status)
        {
            var historyReceiveIncomeReferral = this.Get(id);
            historyReceiveIncomeReferral.Status = status;
            historyReceiveIncomeReferral.ModifiedAt = System.DateTime.UtcNow;
            this.Update(historyReceiveIncomeReferral);
            return 1;
        }

        public int Insert10PercentForUpline(string parentId, decimal amountReceive, string email)
        {
            HistoryReceiveIncome historyReceiveIncome = new HistoryReceiveIncome();
            historyReceiveIncome.UserId = parentId;
            historyReceiveIncome.DateReceive = System.DateTime.UtcNow;
            historyReceiveIncome.Amount = amountReceive;
            historyReceiveIncome.Referral = email;
            historyReceiveIncome.CreatedAt = System.DateTime.UtcNow;
            historyReceiveIncome.ModifiedAt = System.DateTime.UtcNow;
            historyReceiveIncome.IsDeleted = false;
            historyReceiveIncome.Name = parentId;
            historyReceiveIncome.Status = false;
            this.Insert(historyReceiveIncome);
            return 1;
        }
        
    }   
}


