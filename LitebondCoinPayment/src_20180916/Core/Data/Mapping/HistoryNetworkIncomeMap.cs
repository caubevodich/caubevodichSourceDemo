using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;


namespace Core.Data.Mapping
{   
    public class HistoryNetworkIncomeMap : EntityTypeConfiguration<HistoryNetworkIncome>
    {
        public HistoryNetworkIncomeMap()
        {
            ToTable("HistoryNetworkIncome");
            HasKey(c => c.Id);
            Property(c => c.UserId).IsRequired();
            Property(c => c.DateReceive).IsRequired();
            Property(c => c.Amount).IsRequired();
            Property(c => c.Status).IsRequired();
        }
    }
}
