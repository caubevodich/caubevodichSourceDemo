using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;

namespace Core.Data.Mapping
{
    public class ConfigMap : EntityTypeConfiguration<Config>
    {
        public ConfigMap()
        {
            ToTable("Config");
            HasKey(c => c.Id);
            Property(c => c.Name).IsRequired();
        }
    }
}