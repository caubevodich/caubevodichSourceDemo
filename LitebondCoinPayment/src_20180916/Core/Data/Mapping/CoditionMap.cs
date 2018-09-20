using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;

namespace Core.Data.Mapping
{   
    public class CoditionMap : EntityTypeConfiguration<Codition>
    {
        public CoditionMap()
        {
            ToTable("Codition");
            HasKey(c => c.Id);
            Property(c => c.Level).IsRequired();
            Property(c => c.PercentProfitDaily).IsRequired();
            Property(c => c.Day).IsRequired();
            Property(c => c.PercentProfitAndCapital).IsRequired();
            Property(c => c.MinDeposit).HasPrecision(18, 8);
            Property(c => c.MaxDeposit).HasPrecision(18, 8);
            Property(c => c.Condition).IsRequired();
        }
    }
}
