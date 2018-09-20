using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;

namespace Core.Data.Mapping
{
    public class TransactionMap : EntityTypeConfiguration<Transaction>
    {
        public TransactionMap()
        {
            ToTable("Transaction");
            HasKey(c => c.Id);
            Property(c => c.UserId).IsRequired();
            Property(c => c.From).IsRequired();
            Property(c => c.To).IsRequired();
            Property(c => c.Amount).HasPrecision(18, 8);
            Property(c => c.NetworkFee).HasPrecision(18, 8);
            Property(c => c.ExchangeFee).HasPrecision(18, 8);
        }
    }
}