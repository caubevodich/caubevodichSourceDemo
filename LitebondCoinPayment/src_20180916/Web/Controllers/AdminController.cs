using Core.Domain.Entities;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly IBalanceService _balanceService;
        private readonly IWalletService _walletService;

        public AdminController(IAccountService accountService, ITransactionService transactionService,
            IBalanceService balanceService, IWalletService walletService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
            _balanceService = balanceService;
            _walletService = walletService;
        }
        public ActionResult Index()
        {
            var model = new AdminModel();
            model.TransactionWithdraw = GetWithdrawTransactionAll();
            model.TransactionWithdrawWithPending = GetWithdrawTransactionWithPending();
            model.TransactionWithdrawWithConfirmed = GetWithdrawTransactionWithConfirmed();
            return View(model);
        }

        private List<TransactionModel> GetWithdrawTransactionAll()
        {
            var model = new List<TransactionModel>();
            var transaction = _transactionService.All().Where(x => x.Type == false).ToList();
            if (transaction != null)
            {
                foreach (var item in transaction)
                {
                    var walletAddress = _walletService.Find(x => x.UserId == item.UserId).SingleOrDefault().Address;
                    var record = new TransactionModel();
                    record.Id = item.Id;
                    record.UserId = item.UserId;
                    record.From = item.From;
                    record.To = item.To;
                    record.TxId = item.TxId;
                    record.Amount = item.Amount;
                    record.Type = item.Type;
                    record.Status = item.Status;
                    record.TotalConfirm = item.TotalConfirm;
                    record.Network = item.Network;
                    record.NetworkFee = item.NetworkFee;
                    record.ExchangeFee = item.ExchangeFee;
                    record.WalletAddress = walletAddress;
                    record.CreatedAt = item.CreatedAt;
                    record.ModifiedAt = item.ModifiedAt;
                    model.Add(record);
                }
            }
            return model;
        }

        private List<TransactionModel> GetWithdrawTransactionWithPending()
        {
            var model = new List<TransactionModel>();
            var transaction = _transactionService.All().Where(x => x.Type == false && x.Status == "Pending").ToList();
            if (transaction != null)
            {
                foreach (var item in transaction)
                {
                    var walletAddress = _walletService.Find(x => x.UserId == item.UserId).SingleOrDefault().Address;
                    var record = new TransactionModel();
                    record.Id = item.Id;
                    record.UserId = item.UserId;
                    record.From = item.From;
                    record.To = item.To;
                    record.TxId = item.TxId;
                    record.Amount = item.Amount;
                    record.Type = item.Type;
                    record.Status = item.Status;
                    record.TotalConfirm = item.TotalConfirm;
                    record.Network = item.Network;
                    record.NetworkFee = item.NetworkFee;
                    record.ExchangeFee = item.ExchangeFee;
                    record.WalletAddress = walletAddress;
                    record.CreatedAt = item.CreatedAt;
                    record.ModifiedAt = item.ModifiedAt;
                    model.Add(record);
                }
            }
            return model;
        }

        private List<TransactionModel> GetWithdrawTransactionWithConfirmed()
        {
            var model = new List<TransactionModel>();
            var transaction = _transactionService.All().Where(x => x.Type == false && x.Status == "Confirmed").ToList();
            if (transaction != null)
            {
                foreach (var item in transaction)
                {
                    var walletAddress = _walletService.Find(x => x.UserId == item.UserId).SingleOrDefault().Address;
                    var record = new TransactionModel();
                    record.Id = item.Id;
                    record.UserId = item.UserId;
                    record.From = item.From;
                    record.To = item.To;
                    record.TxId = item.TxId;
                    record.Amount = item.Amount;
                    record.Type = item.Type;
                    record.Status = item.Status;
                    record.TotalConfirm = item.TotalConfirm;
                    record.Network = item.Network;
                    record.NetworkFee = item.NetworkFee;
                    record.ExchangeFee = item.ExchangeFee;
                    record.WalletAddress = walletAddress;
                    record.CreatedAt = item.CreatedAt;
                    record.ModifiedAt = item.ModifiedAt;
                    model.Add(record);
                }
            }
            return model;
        }

        [HttpPost]
        public JsonResult updateStatusTransactionToConfirmed(string id, string txid, string status)
        {
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(txid))
            {
                var idInt = Int64.Parse(id);
                var checkIdInTableData = _transactionService.Find(x => x.Id == idInt).SingleOrDefault();
                var takeID = checkIdInTableData.Id;
                var takeTxIdInTableData = checkIdInTableData.TxId;
                var takeStatus = checkIdInTableData.Status;
                if (idInt > 0 && checkIdInTableData != null)
                {
                    if (takeStatus == "Pending")
                    {
                        checkIdInTableData.Status = "Confirmed";
                        checkIdInTableData.CreatedAt = DateTime.UtcNow;
                        checkIdInTableData.TxId = txid.Trim();
                        _transactionService.Update(checkIdInTableData);
                    }
                    return Json(new { IsError = false, message = "Current Status is a confirm." });
                }
                return Json(new { IsError = false, message = "Fail ! Please check Id !" });
            }
            return Json(new { IsError = false, message = "ID and TXID are not empty ! " });
        }





    }
}