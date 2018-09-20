using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;

namespace Core.Data.Mapping
{   
    public class HistoryGetProfitBalanceMap : EntityTypeConfiguration<HistoryGetProfitBalance>
    {
        public HistoryGetProfitBalanceMap()
        {
            ToTable("HistoryGetProfitBalance");
            HasKey(c => c.Id);
            Property(c => c.UserId).IsRequired();
            Property(c => c.DateGetBalance).IsRequired();
            Property(c => c.Amount).IsRequired();
        }
    }
}
