using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;

namespace Core.Data.Mapping
{    
    public class HistoryGetIncomeBalanceMap : EntityTypeConfiguration<HistoryGetIncomeBalance>
    {
        public HistoryGetIncomeBalanceMap()
        {
            ToTable("HistoryGetIncomeBalance");
            HasKey(c => c.Id);
            Property(c => c.UserId).IsRequired();
            Property(c => c.DateGetBalance).IsRequired();
            Property(c => c.Amount).HasPrecision(18, 8);
        }
    }
}
