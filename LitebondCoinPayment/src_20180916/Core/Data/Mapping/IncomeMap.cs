using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;

namespace Core.Data.Mapping
{   
    public class IncomeMap : EntityTypeConfiguration<Income>
    {
        public IncomeMap()
        {
            ToTable("Income");
            HasKey(c => c.Id);
            Property(c => c.UserId).IsRequired();
            Property(c => c.TotalAmountIncome).HasPrecision(18, 8);
            Property(c => c.DateReceive).IsRequired();
        }
    }
}
