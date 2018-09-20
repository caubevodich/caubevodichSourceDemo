using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;

namespace Core.Data.Mapping
{
    public class ProfitDailyMap : EntityTypeConfiguration<ProfitDaily>
    {
        public ProfitDailyMap()
        {
            ToTable("ProfitDaily");
            HasKey(c => c.Id);
            Property(c => c.DepositId).IsRequired();
            Property(c => c.DateReceive).IsRequired();
            Property(c => c.AmountDaily).HasPrecision(18, 8);
            Property(c => c.Status).IsRequired();
        }
    }
}
