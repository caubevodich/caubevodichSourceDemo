using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Core.BlockIO;
using Core.Domain.Entities;
using Core.Services;
using Google.Authenticator;
using Newtonsoft.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class WalletController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly IBalanceService _balanceService;
        private readonly IWalletService _walletService;
        public WalletController(IAccountService accountService, ITransactionService transactionService,
            IBalanceService balanceService, IWalletService walletService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
            _balanceService = balanceService;
            _walletService = walletService;
        }

        // GET: Wallet
        public ActionResult Index()
        {
            //Check Balance Record
            var balanceCheck = _balanceService.CheckTableBalanceInDatabase(User.Identity.Name);
            if (!balanceCheck) { _balanceService.InsertBalanceRecord(User.Identity.Name); }
            //CheckDep
            CheckDeposit();


            var wallet = _accountService.GetWallet(User.Identity.Name);
            return View(wallet.Data);
        }


        public void CheckDeposit()
        {
            var blockIO = new BlockIO();
            //get Address in Wallet to compare
            var getAddressInWallet = _walletService.GetByEmail(User.Identity.Name);
            if (getAddressInWallet != null)
            {
                //get list deposit
                var getlistDep = blockIO.getAllTransactionDeposit(getAddressInWallet.Address).Data;
                var valueData = getlistDep.Where(x => x.Key == "txs").ToList();
                var valueNetwork = getlistDep.Where(x => x.Key == "network").ToList().Find(x => x.Key == "network").Value.ToString();
                var valueData2 = valueData.GroupBy(x => x.Value).GroupBy(x => x.Key).ToList()[0].Key;
                string json = JsonConvert.SerializeObject(valueData2);
                var RootObjects = JsonConvert.DeserializeObject<List<JsonDepositModel>>(json);
                if (RootObjects.Count > 0)
                {
                    foreach (var item in RootObjects)
                    {
                        //check record in transaction
                        var checkRecordInDB = _transactionService.GetByEmailAmountTxid(User.Identity.Name, item.txid);
                        var amount = item.amounts_received.FirstOrDefault(x => x.recipient == getAddressInWallet.Address).amount;
                        if (checkRecordInDB == null && amount > 0 )
                        {
                            //Insert transaction                        
                            var transaction = new Transaction();
                            transaction.Name = User.Identity.Name;
                            transaction.UserId = User.Identity.Name;
                            transaction.From = "";
                            transaction.To = "";
                            transaction.TxId = item.txid;
                            transaction.Amount = amount;
                            transaction.CreatedAt = DateTime.UtcNow;
                            transaction.ModifiedAt = DateTime.UtcNow;
                            transaction.TotalConfirm = 0;
                            transaction.Type = true;
                            transaction.Network = valueNetwork;
                            transaction.NetworkFee = 0;  //tinhsau
                            transaction.ExchangeFee = 0;//tinhsau
                            transaction.Status = "Pending";
                            transaction.IsDeleted = false;
                            _transactionService.Insert(transaction);

                        }
                        else
                        {
                            var balance = _balanceService.GetByEmail(User.Identity.Name);
                            if (balance != null)
                            {
                                if (checkRecordInDB.Status == "Pending" && checkRecordInDB.Amount > 0 && item.confirmations > 2 && checkRecordInDB.Type == true)
                                {
                                    //Update Balance
                                    var totalBalance = _balanceService.GetTotalBalanceByEmail(User.Identity.Name);
                                    var amountUp = totalBalance + amount;
                                    var addAmountToBalance = _balanceService.UpdateBalance(User.Identity.Name, amountUp);
                                    if (addAmountToBalance == 1)
                                    {
                                        //Update Status
                                        var findTransaction = _transactionService.GetByEmailAmountTxid(User.Identity.Name, item.txid);
                                        findTransaction.Status = "Confirmed";
                                        findTransaction.ModifiedAt = DateTime.UtcNow;
                                        _transactionService.Update(findTransaction);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        [HttpPost]
        public JsonResult getNewAddress()
        {
            var blockIO = new BlockIO();
            string label = User.Identity.Name + "_" + DateTime.Now.ToString("ddMMyyyyhhmmsstt");
            var response = blockIO.getNewAddress(label);

            if (response.IsSuccess)
            {
                var entries = response.Data.Select(d => $"{d.Key}: \"{ string.Join(",", d.Value)}\"");
                string json = "{" + string.Join(",", entries) + "}";

                var address = Address.ToAddress(json);
                if (address != null)
                {
                    _accountService.CreateNewWallet(User.Identity.Name, new Wallet
                    {
                        Name = label,
                        Address = address.WalletAddress,
                        Network = address.Network,
                    });
                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getBalance()
        {
            var blockIO = new BlockIO();
            var wallet = _accountService.GetActiveWallet(User.Identity.Name);
            if (wallet != null)
            {
                var response = blockIO.getAddressBalance(wallet.Address);

                if (response.IsSuccess)
                {
                    string available_balance_Text = response.Data["available_balance"].ToString();
                    decimal available_balance = decimal.Parse(available_balance_Text, CultureInfo.InvariantCulture);
                    string pending_received_balance_Text = response.Data["pending_received_balance"].ToString();
                    decimal pending_received_balance = decimal.Parse(pending_received_balance_Text, CultureInfo.InvariantCulture);
                    wallet.AvailableBalance = available_balance;
                    wallet.PendingReceivedBalance = pending_received_balance;

                    _accountService.SyncWallet(User.Identity.Name, wallet);
                }
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }

        [HttpPost]
        public JsonResult Widthdraw(string address, string amount, string twoFactor)
        {
            //decimal percentFee = (decimal)0.05;

            //var wallet = _accountService.GetActiveWallet(User.Identity.Name);
            //if (wallet != null)
            //{
            //    ////BlockIO blockIO = new BlockIO();
            //    ////var response = blockIO.withdrawFromAddresses(new List<string> { wallet.Address }, new List<string> { address }, new List<string> { amount });
            //    ////if (response.IsSuccess)
            //    ////{
            //    ////    var transaction = new Transaction();
            //    ////    transaction.TxId = response.Data["txid"].ToString();
            //    ////    transaction.NetworkFee = decimal.Parse(response.Data["network_fee"].ToString(), CultureInfo.InvariantCulture);

            //    ////    transaction.TotalConfirm = 0;
            //    ////    _accountService.Withdraw(wallet, address, decimal.Parse(amount, CultureInfo.InvariantCulture), transaction);
            //    ////}

            //    var transaction = new Transaction();
            //    //  transaction.TxId = response.Data["txid"].ToString();
            //    // transaction.NetworkFee = decimal.Parse(response.Data["network_fee"].ToString(), CultureInfo.InvariantCulture);
            //    transaction.Status = "Pending";
            //    transaction.TotalConfirm = 0;
            //    // transaction.ExchangeFee = amount * percentFee / 100;

            //    decimal withdrawAmout = decimal.Parse(amount, CultureInfo.InvariantCulture);
            //    var exchangeFee = percentFee * withdrawAmout;
            //    var amoutReceive = withdrawAmout - exchangeFee;
            //    transaction.ExchangeFee = exchangeFee;

            //    _accountService.Withdraw(wallet, address, amoutReceive, transaction);

            //    return Json(new { Ok = 1 }, JsonRequestBehavior.AllowGet);
            //}
            //return Json(new { errror = "Wallet not found" }, JsonRequestBehavior.AllowGet);

            var wallet = _accountService.GetActiveWallet(User.Identity.Name);
            var totalBalance = _balanceService.GetByEmail(User.Identity.Name).TotalBalance;
            if (!string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(amount) && !string.IsNullOrEmpty(twoFactor) && !(amount == "NaN"))
            {
                var amountP = decimal.Parse(amount.Replace(".", ",").ToString());
                if (amountP <= totalBalance && amountP >= (decimal)0.05)
                {
                    //Check 2FA
                    var currentUser = _accountService.GetByEmail(User.Identity.Name);
                    var authencator = new TwoFactorAuthenticator(true, true);
                    string authenKey = currentUser.Email;
                    var isValid = authencator.ValidateTwoFactorPIN(authenKey, twoFactor);
                    if (isValid)
                    {
                        //Update Balance
                        var amoutInput = totalBalance - amountP;
                        _balanceService.UpdateBalance(User.Identity.Name, amoutInput);
                        //Insert Transaction
                        var transaction = new Transaction();
                        transaction.Name = User.Identity.Name;
                        transaction.UserId = User.Identity.Name;
                        transaction.From = "";
                        transaction.To = "";
                        transaction.TxId = "";
                        transaction.Amount = amountP;
                        transaction.CreatedAt = DateTime.UtcNow;
                        transaction.ModifiedAt = DateTime.UtcNow;
                        transaction.TotalConfirm = 0;
                        transaction.Type = false;
                        transaction.Network = wallet.Network;
                        transaction.NetworkFee = 0;  //tinhsau
                        transaction.ExchangeFee = 0;
                        transaction.Status = "Pending";
                        transaction.IsDeleted = false;
                        _transactionService.Insert(transaction);

                        return Json(new { IsError = true, Ok = 1, message = "Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { IsError = false, error = "Fail ! Please ! Input correct 2FA." }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsError = false, error = "Fail ! Minimum withdraw is 0.05 LTC ! Please check again." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsError = false, error = "Please ! Input all correct !" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTxtConfirmed(int transactionId)
        {
            var transaction = _transactionService.Get(transactionId);
            ChainSO chainSO = new ChainSO();
            var result = chainSO.GetTxtConfirmed(transaction.Network, transaction.TxId);
            transaction.TotalConfirm = result;
            //if (result > 2)
            //{
            //    transaction.Status = "Confirmed";
            //}
            _transactionService.Update(transaction);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}