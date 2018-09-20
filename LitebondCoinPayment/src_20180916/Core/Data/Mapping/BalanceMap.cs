using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Mapping
{
    public class BalanceMap : EntityTypeConfiguration<Balance>
    {
        public BalanceMap()
        {
            ToTable("Balance");
            HasKey(c => c.Id);
            Property(c => c.UserId).IsRequired();            
            Property(c => c.TotalBalance).HasPrecision(18, 8);           
        }
    }
}
