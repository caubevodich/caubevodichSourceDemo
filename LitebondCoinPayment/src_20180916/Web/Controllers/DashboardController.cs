using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Core.Domain.Entities;
using Core.Domain.Models;
using Core.Services;
using WebUI.Helpers;

namespace WebUI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IHistoryDepositService _historyDepositService;
        private readonly IProfitDailyService _profitDailyService;
        private readonly IHistoryNetworkIncomeService _historyNetworkIncomeService;
        private readonly IIncomeService _incomeService;
        private readonly IProfitService _profitService;
        private readonly IReceiveNetworkComissionService _receiveNetworkComissionService;
        private readonly ICoditionService _coditionService;
        private readonly AccountServiceExtension _accountServiceExtension;
        private readonly IBalanceService _balanceService;
        private readonly INetworkComissionService _networkComissionService;

        public DashboardController(IAccountService accountService,
                            IHistoryDepositService historyDepositService,
                            IProfitDailyService profitDailyService,
                            IHistoryNetworkIncomeService historyNetworkIncomeService,
                            IIncomeService incomeService,
                            IProfitService profitService,
                            IReceiveNetworkComissionService receiveNetworkComissionService,
                            ICoditionService coditionService,
                            IBalanceService balanceService,
                            INetworkComissionService networkComissionService)
        {
            _accountService = accountService;
            _historyDepositService = historyDepositService;
            _profitDailyService = profitDailyService;
            _historyNetworkIncomeService = historyNetworkIncomeService;
            _incomeService = incomeService;
            _profitService = profitService;
            _receiveNetworkComissionService = receiveNetworkComissionService;
            _coditionService = coditionService;
            _balanceService = balanceService;
            _networkComissionService = networkComissionService;

            this._accountService = accountService;
            _accountServiceExtension = new AccountServiceExtension();
        }

        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            var purchase = await Core.CoinPayment.CoinpaymentsApi.CreateTransaction(10, "USD", "LTCT", "peusa@outlook.com");
            var bb = "";
            //ABCC();

            //-------------------------------------------------------//
            //Step 1//// Check and Create Data Balance
            var balanceCheck = _balanceService.CheckTableBalanceInDatabase(User.Identity.Name);
            if (!balanceCheck) { _balanceService.InsertBalanceRecord(User.Identity.Name); }

            //Step 2////Check and Create Data ProfitTable
            var profitCheck = _profitService.CheckTableProfitInDatabase(User.Identity.Name);
            if (!profitCheck) { _profitService.InsertRecordToProfitDatabase(User.Identity.Name, new Profit()); }

            //Step 3////Check and Create Data IncomeTable
            var incomeCheck = _incomeService.CheckTableIncomeInDatabase(User.Identity.Name);
            if (!incomeCheck) { _incomeService.InsertRecordToIncomeDatabase(User.Identity.Name, new Income()); }

            //Step 4////Check and Create Data ReceiveNetworkComission
            var receiveNetworkComissionCheck = _receiveNetworkComissionService.CheckTableReceiveNetworkComissionInDatabase(User.Identity.Name);
            if (!receiveNetworkComissionCheck) { _receiveNetworkComissionService.InsertRecordToReceiveNetworkComissionDatabase(User.Identity.Name, new ReceiveNetworkComission()); }

            //Step 5//// Check Profit daily
            checkProfitAndInsertProfitDaily();

            //Step 6////Check Comission 
            checkReceiveNetworkComission();

            //Step 7////Check and Update Level for User
            var checkLevelUser = _accountService.CheckAndUpdateLevel(User.Identity.Name, _accountService.GetByEmail(User.Identity.Name).Level);
            if (_accountService.GetByEmail(User.Identity.Name).Level != checkLevelUser)
            {
                var accLevel = _accountServiceExtension.GetByEmail(User.Identity.Name);
                accLevel.Level = checkLevelUser;
               // _accountServiceExtension.Update(accLevel);
                _accountService.UpdateLevel(checkLevelUser, User.Identity.Name);
            }

            //-------------------------------------------------------//

            //// handle ReferralLink and Wallet    
            var walletResult = _accountService.GetWallet(User.Identity.Name);
            var accountInfo = _accountService.GetByEmail(User.Identity.Name);
            var wallet = walletResult.Data;
            string referralLink = "";
            if (accountInfo != null)
            {
                if (!string.IsNullOrWhiteSpace(accountInfo.ReferralLink)) { referralLink = accountInfo.ReferralLink; }
                else
                {
                    referralLink = Helpers.Helpers.GetReferralLinkFromEmail(accountInfo.Email);
                    accountInfo.ReferralLink = referralLink;
                    _accountService.Update(accountInfo);
                }
            }
            if (!string.IsNullOrWhiteSpace(referralLink))
            {
                referralLink = "http://litebond.co/?referral=" + referralLink;
            }
            var model = new DashboardModel
            {
                //Balance = wallet.AvailableBalance ?? 0,
                Balance = _balanceService.GetByEmail(User.Identity.Name).TotalBalance,
                MyWallet = wallet,
                ReferralLink = referralLink,
                Deposit = _historyDepositService.calTotalDeposit(User.Identity.Name),
                Level = _accountService.GetByEmail(User.Identity.Name).Level,
                TotalReferral = _networkComissionService.GetTotalRef(User.Identity.Name)

            };
            return View(model);
        }


        private void checkProfitAndInsertProfitDaily()
        {
            var listHistoryDeposit = _historyDepositService.ListHistoryDeposit(User.Identity.Name);
            var now = DateTime.UtcNow;
            if (listHistoryDeposit != null)
            {
                //Step 1//// Pay profit daily
                foreach (var item in listHistoryDeposit)
                {
                    TimeSpan ts = now - item.DateCreate;
                    var numTs = Convert.ToInt32(ts.Days);
                    TimeSpan ts2 = item.DateEnd - item.DateCreate;
                    var numTs2 = Convert.ToInt32(ts2.Days);
                    var numProfDaily = ((decimal)item.PercentProfitDaily * item.Amount) / 100;
                    if (numTs <= numTs2)
                    {
                        for (int i = 0; i <= numTs; i++)
                        {
                            var listProfitDaily = _profitDailyService.ListProfitDaily(User.Identity.Name, item.Id).Count;
                            if (listProfitDaily < numTs)
                            {
                                TimeSpan subDay = new TimeSpan(i, 0, 0, 0);
                                _profitDailyService.InsertRecordProfitDaily(item.Id, User.Identity.Name, numProfDaily, DateTime.UtcNow.Subtract(subDay));
                            }
                        }
                    }
                }
                //Step 2//// Check DateEnd and Update Status
                foreach (var item in listHistoryDeposit)
                {
                    if (now >= item.DateEnd)
                    {
                        _historyDepositService.updateStatusEndDep(User.Identity.Name, item.Id);
                    }
                }
            }
        }
        private void checkReceiveNetworkComission()
        {
            
            //get date receive comission
            var getDateReceiveComission = _receiveNetworkComissionService.GetByEmail(User.Identity.Name).DateReceiveComission;
            //get AmountRemain comission
            var getAmountRemainComission = _receiveNetworkComissionService.GetByEmail(User.Identity.Name).AmountRemain;
            //calculate total deposit of User
            var totalDepOfUser = _historyDepositService.calTotalDeposit(User.Identity.Name);
            //calculate total deposit of total network from date receive comission
            var totalDepOfNetwork = _accountService.totalDepOfTotalNetwork(User.Identity.Name, getDateReceiveComission);
            //check percent commision user will take
            var percentCommisonReceived = _coditionService.GetPercentComission(User.Identity.Name, totalDepOfNetwork);
            //get returnTime of Amount
            var returnTime = returnTimes(User.Identity.Name, totalDepOfNetwork);


            var calculateNetworkComission = (((decimal)percentCommisonReceived * totalDepOfUser) / 100);
            var saleAmountRemain = (totalDepOfNetwork + getAmountRemainComission) - (totalDepOfUser * (decimal)returnTime);
            if (returnTime != 0 && saleAmountRemain > 0)
            {
                if (checkReceiveCommission(User.Identity.Name, totalDepOfNetwork))
                {
                    //Insert table HistoryNetworkIncome            
                    var insertIntoHistoryNetworkIncome = _historyNetworkIncomeService.InsertHistoryNetworkIncome(User.Identity.Name, calculateNetworkComission);
                    //Update date and amount remain
                    var updateDateAndAmountRemain = _receiveNetworkComissionService.UpdateDateReceiveCommisonAndSaleAmountRemain(User.Identity.Name, saleAmountRemain);
                }
            }


            decimal amountRemain = 0;
            do
            {
                //get AmountRemain comission
                var getAmountRemainComissionAgian = _receiveNetworkComissionService.GetByEmail(User.Identity.Name).AmountRemain;
                var getPercentCommison = _coditionService.GetPercentComission(User.Identity.Name, getAmountRemainComissionAgian);
                var getReturnTime = returnTimes(User.Identity.Name, getAmountRemainComissionAgian);
                var calNetworkComission = (((decimal)getPercentCommison * totalDepOfUser) / 100);
                amountRemain = getAmountRemainComissionAgian - (totalDepOfUser * (decimal)getReturnTime);
                if (getReturnTime != 0 && amountRemain > 0)
                {
                    if (checkReceiveCommission(User.Identity.Name, getAmountRemainComissionAgian))
                    {
                        //Insert table HistoryNetworkIncome            
                        var insert = _historyNetworkIncomeService.InsertHistoryNetworkIncome(User.Identity.Name, calNetworkComission);
                        //Update date and amount remain
                        var updateDate = _receiveNetworkComissionService.UpdateDateReceiveCommisonAndSaleAmountRemain(User.Identity.Name, amountRemain);
                    }
                }
            } while (checkReceiveCommission(User.Identity.Name, amountRemain));

        }

        private bool checkReceiveCommission(string email, decimal total)
        {
            if (total >= _coditionService.G(email) && total > 0)
            {
                return true;
            }
            else if (_coditionService.F(email) <= total && total < _coditionService.G(email) && total > 0)
            {
                return true;
            }
            else if (_coditionService.E(email) <= total && total < _coditionService.F(email) && total > 0)
            {
                return true;
            }
            else if (_coditionService.D(email) <= total && total < _coditionService.E(email) && total > 0)
            {
                return true;
            }
            else if (_coditionService.C(email) <= total && total < _coditionService.D(email) && total > 0)
            {
                return true;
            }
            else if (_coditionService.B(email) <= total && total < _coditionService.C(email) && total > 0)
            {
                return true;
            }
            else if (_coditionService.A(email) <= total && total < _coditionService.B(email) && total > 0)
            {
                return true;
            }
            return false;
        }

        private int returnTimes(string email, decimal amount)
        {
            if (amount >= _coditionService.G(email) && amount > 0)
            {
                return 100;
            }
            else if (_coditionService.F(email) <= amount && amount < _coditionService.G(email) && amount > 0)
            {
                return 70;
            }
            else if (_coditionService.E(email) <= amount && amount < _coditionService.F(email) && amount > 0)
            {
                return 50;
            }
            else if (_coditionService.D(email) <= amount && amount < _coditionService.E(email) && amount > 0)
            {
                return 30;
            }
            else if (_coditionService.C(email) <= amount && amount < _coditionService.D(email) && amount > 0)
            {
                return 20;
            }
            else if (_coditionService.B(email) <= amount && amount < _coditionService.C(email) && amount > 0)
            {
                return 10;
            }
            else if (_coditionService.A(email) <= amount && amount < _coditionService.B(email) && amount > 0)
            {
                return 5;
            }
            return 0;
        }

        [HttpPost]
        public JsonResult GetBalanceOfUser()
        {
            var balance = _balanceService.GetByEmail(User.Identity.Name).TotalBalance;                      
            return Json(balance, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetReferralLink()
        {
            var referral = _accountService.GetByEmail(User.Identity.Name).ReferralLink;            
            return Json(referral, JsonRequestBehavior.AllowGet);
        }


        public async void ABCC()
        { 
            var purchase = await Core.CoinPayment.CoinpaymentsApi.CreateTransaction(10, "USD", "LTCT", "peusa@outlook.com");
            var bb = "";
        }

    }
}