using Core.Data;
using Core.Domain.Entities;
using System.Linq;

namespace Core.Services
{
    public interface ITransactionService : IEntityService<Transaction>
    {
        Transaction GetByEmailAmountTxid(string email, string txid);
    }

    public class TransactionService : EntityService<Transaction>, ITransactionService
    {
        public TransactionService(IDbContext context) : base(context)
        {
        }

        public Transaction GetByEmailAmountTxid(string email, string txid)
        {
            var transaction = Table.SingleOrDefault(x => x.UserId == email && x.TxId == txid && x.Type == true);
            return transaction;
        }
    }
}