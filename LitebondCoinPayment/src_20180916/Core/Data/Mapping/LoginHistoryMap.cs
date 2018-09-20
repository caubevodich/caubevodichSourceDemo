using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;

namespace Core.Data.Mapping
{
    public class LoginHistoryMap : EntityTypeConfiguration<LoginHistory>
    {
        public LoginHistoryMap()
        {
            ToTable("LoginHistory");
            HasKey(c => c.Id);
            Property(c => c.UserId).IsRequired();
        }
    }
}