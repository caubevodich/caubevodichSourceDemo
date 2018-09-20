using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;

namespace Core.Data.Mapping
{    
    public class HistoryDepositMap : EntityTypeConfiguration<HistoryDeposit>
    {
        public HistoryDepositMap()
        {
            ToTable("HistoryDeposit");
            HasKey(c => c.Id);
            Property(c => c.UserId).IsRequired();
            Property(c => c.DateCreate).IsRequired();
            Property(c => c.Amount).HasPrecision(18, 8);
            Property(c => c.TotalDay).IsRequired();
            Property(c => c.PercentProfitDaily).IsRequired();
        }
    }
}
