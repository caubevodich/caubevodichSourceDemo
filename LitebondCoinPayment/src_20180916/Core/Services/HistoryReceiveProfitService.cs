using Core.Data;
using Core.Domain.Entities;


namespace Core.Services
{
    public interface IHistoryReceiveProfitService : IEntityService<HistoryReceiveProfit>
    {
    }
    public class HistoryReceiveProfitService : EntityService<HistoryReceiveProfit>, IHistoryReceiveProfitService
    {
        public HistoryReceiveProfitService(IDbContext context) : base(context)
        {
        }
    }
}
