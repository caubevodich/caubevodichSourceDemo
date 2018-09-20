using System;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class AdminModel
    {
        public IList<TransactionModel> TransactionWithdraw { get; set; }
        public IList<TransactionModel> TransactionWithdrawWithPending { get; set; }
        public IList<TransactionModel> TransactionWithdrawWithConfirmed { get; set; }
    }

    public class TransactionModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TxId { get; set; }
        public decimal Amount { get; set; }
        public bool Type { get; set; }
        public string Status { get; set; }
        public int TotalConfirm { get; set; }
        public string Network { get; set; }
        public decimal NetworkFee { get; set; }
        public decimal ExchangeFee { get; set; }
        public string WalletAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }      
        public bool IsDeleted { get; set; }
    }

}