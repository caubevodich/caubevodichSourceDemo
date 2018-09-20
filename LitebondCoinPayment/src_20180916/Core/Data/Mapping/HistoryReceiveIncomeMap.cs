using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;


namespace Core.Data.Mapping
{   
    public class HistoryReceiveIncomeMap : EntityTypeConfiguration<HistoryReceiveIncome>
    {
        public HistoryReceiveIncomeMap()
        {
            ToTable("HistoryReceiveIncome");
            HasKey(c => c.Id);
            Property(c => c.UserId).IsRequired();
            Property(c => c.DateReceive).IsRequired();
            Property(c => c.Amount).HasPrecision(18, 8);
            Property(c => c.Status).IsRequired();
        }
    }
}
