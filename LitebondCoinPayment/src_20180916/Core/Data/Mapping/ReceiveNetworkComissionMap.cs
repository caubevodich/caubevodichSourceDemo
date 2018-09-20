using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;


namespace Core.Data.Mapping
{
    public class ReceiveNetworkComissionMap : EntityTypeConfiguration<ReceiveNetworkComission>
    {
        public ReceiveNetworkComissionMap()
        {
            ToTable("ReceiveNetworkComission");
            HasKey(c => c.Id);
            Property(c => c.UserId).IsRequired();
            Property(c => c.DateReceiveComission).IsRequired();
            Property(c => c.AmountRemain).HasPrecision(18, 8);
        }
    }
}
