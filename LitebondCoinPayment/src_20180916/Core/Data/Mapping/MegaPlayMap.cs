using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;

namespace Core.Data.Mapping
{
    public class MegaPlayMap : EntityTypeConfiguration<MegaPlay>
    {
        public MegaPlayMap()
        {
            ToTable("MegaPlay");
            HasKey(c => c.Id);
            Property(c => c.PlayNumber).IsRequired();
        }
    }
}