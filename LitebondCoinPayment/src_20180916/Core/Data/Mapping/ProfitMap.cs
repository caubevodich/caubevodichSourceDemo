using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;

namespace Core.Data.Mapping
{    
    public class ProfitMap : EntityTypeConfiguration<Profit>
    {
        public ProfitMap()
        {
            ToTable("Profit");
            HasKey(c => c.Id);
            Property(c => c.UserId).IsRequired();
            Property(c => c.TotalAmountProfit).HasPrecision(18, 8);
            Property(c => c.DateReceive).IsRequired();
        }
    }
}
