using System.Data.Entity.ModelConfiguration;
using Core.Domain.Entities;

namespace Core.Data.Mapping
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            ToTable("Account");
            HasKey(c => c.Id);
            Property(c => c.Balance).HasPrecision(18, 8);
            Property(c => c.Email).IsRequired();
            Property(c => c.PasswordHash).IsRequired();
        }
    }
}