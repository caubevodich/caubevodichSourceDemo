using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;

namespace Core.Data.Mapping
{
    public class WalletMap : EntityTypeConfiguration<Wallet>
    {
        public WalletMap()
        {
            ToTable("Wallet");
            HasKey(c => c.Id);
            Property(c => c.Address).IsRequired();
            Property(c => c.UserId).IsRequired();
            Property(x => x.AvailableBalance).HasPrecision(18, 8);
            Property(x => x.PendingReceivedBalance).HasPrecision(18, 8);
        }
    }
}