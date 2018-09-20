using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Core.Domain.Entities;
using Core.Domain.Models;

namespace Core.Services
{
    public interface IReceiveNetworkComissionService : IEntityService<ReceiveNetworkComission>
    {
        ReceiveNetworkComission GetByEmail(string email);
        bool CheckTableReceiveNetworkComissionInDatabase(string email);
        int InsertRecordToReceiveNetworkComissionDatabase(string email, ReceiveNetworkComission receive);
        int UpdateDateReceiveCommisonAndSaleAmountRemain(string email, decimal amountRemain);
        int UpdateSaleAmountRemain(string email, decimal saleAmountRemain);
        int UpdateDateReceiveCommison(string email);
    }

    public class ReceiveNetworkComissionService : EntityService<ReceiveNetworkComission>, IReceiveNetworkComissionService
    {
        public ReceiveNetworkComissionService(IDbContext context) : base(context)
        {
        }

        public ReceiveNetworkComission GetByEmail(string email)
        {
            var receiveNetworkComission = Table.SingleOrDefault(x => x.UserId == email);
            return receiveNetworkComission;
        }

        public bool CheckTableReceiveNetworkComissionInDatabase(string email)
        {
            bool check = false;
            var recordExist = this.Find(x => x.UserId == email).FirstOrDefault();
            if (recordExist != null)
            {
                return true;
            }
            return check;
        }
        public int InsertRecordToReceiveNetworkComissionDatabase(string email, ReceiveNetworkComission receive)
        {
            int result = 0;
            if (!CheckTableReceiveNetworkComissionInDatabase(email))
            {
                receive.UserId = email;
                receive.DateReceiveComission = System.DateTime.UtcNow;
                receive.AmountRemain = 0;
                receive.CreatedAt = System.DateTime.UtcNow;
                receive.ModifiedAt = System.DateTime.UtcNow;
                receive.Name = email;
                receive.IsDeleted = false;
                this.Insert(receive);
                return 1;
            }
            return result;
        }
        public int UpdateDateReceiveCommisonAndSaleAmountRemain(string email, decimal saleAmountRemain)
        {
            var rec = this.Find(x => x.UserId == email).SingleOrDefault();
            rec.DateReceiveComission = System.DateTime.UtcNow;
            rec.AmountRemain = saleAmountRemain;
            this.Update(rec);
            return 1;
        }

        public int UpdateSaleAmountRemain(string email, decimal saleAmountRemain)
        {
            var rec = this.Find(x => x.UserId == email).SingleOrDefault();
            rec.AmountRemain = saleAmountRemain;
            this.Update(rec);
            return 1;
        }
        public int UpdateDateReceiveCommison(string email)
        {
            var rec = this.Find(x => x.UserId == email).SingleOrDefault();
            rec.DateReceiveComission = System.DateTime.UtcNow;
            this.Update(rec);
            return 1;
        }
    }
}
