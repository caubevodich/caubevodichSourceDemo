using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;

namespace Core.Data.Mapping
{
    public class HistoryReceiveProfitMap : EntityTypeConfiguration<HistoryReceiveProfit>
    {
        public HistoryReceiveProfitMap()
        {
            ToTable("HistoryReceiveProfit");
            HasKey(c => c.Id);
            Property(c => c.UserId).IsRequired();
            Property(c => c.DateReceive).IsRequired();
            Property(c => c.AmountProfit).HasPrecision(18, 8);
        }
    }
}
