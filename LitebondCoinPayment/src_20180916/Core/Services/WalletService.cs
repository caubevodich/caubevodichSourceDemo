using Core.Data;
using Core.Domain.Entities;
using System.Linq;

namespace Core.Services
{
    public interface IWalletService : IEntityService<Wallet>
    {
        Wallet GetByEmail(string email);
    }

    public class WalletService : EntityService<Wallet>, IWalletService
    {
        public WalletService(IDbContext context) : base(context)
        {
        }

        public Wallet GetByEmail(string email)
        {
            var wallet = Table.FirstOrDefault(x => x.UserId == email);
            return wallet;
        }
      
    }
}