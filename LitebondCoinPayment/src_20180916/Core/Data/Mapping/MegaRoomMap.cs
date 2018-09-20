using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;

namespace Core.Data.Mapping
{
    public class MegaRoomMap : EntityTypeConfiguration<MegaRoom>
    {
        public MegaRoomMap()
        {
            ToTable("MegaRoom");
            HasKey(c => c.Id);
            Property(c => c.Name).IsRequired();
        }
    }
}