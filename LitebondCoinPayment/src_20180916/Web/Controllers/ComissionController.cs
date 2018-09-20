using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Domain.Entities;
using Core.Domain.Models;
using Core.Services;
using Microsoft.AspNet.Identity.Owin;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class ComissionController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly INetworkComissionService _networkComissionService;
        private readonly IHistoryNetworkIncomeService _historyNetworkIncomeService;
        private readonly IHistoryReceiveIncomeService _historyReceiveIncomeService;
        private readonly IIncomeService _incomeService;
        private readonly IProfitService _profitService;
        private readonly IProfitDailyService _profitDailyService;
        private readonly IProfitComissionService _profitComissionService;
        private readonly IHistoryDepositService _historyDepositService;
        private readonly ICoditionService _coditionService;
        private readonly IHistoryGetIncomeBalanceService _historyGetIncomeBalanceService;
        private readonly IHistoryGetProfitBalanceService _historyGetProfitBalanceService;
        private readonly IBalanceService _balanceService;
        private readonly AccountServiceExtension _accountServiceExtension;

        public ComissionController(
                            IAccountService accountService,
                            INetworkComissionService networkcomissionService,
                            IHistoryNetworkIncomeService historyNetworkIncomeService,
                             IHistoryReceiveIncomeService historyReceiveIncomeService,
                             IIncomeService incomeService,
                             IProfitService profitService,
                             IProfitComissionService profitComissionService,
                             IProfitDailyService profitDailyService,
                             IHistoryDepositService historyDepositService,
                             ICoditionService coditionService,
                             IHistoryGetIncomeBalanceService historyGetIncomeBalanceService,
                             IHistoryGetProfitBalanceService historyGetProfitBalanceService,
                             IBalanceService balanceService)
        {
            _accountService = accountService;
            _networkComissionService = networkcomissionService;
            _historyNetworkIncomeService = historyNetworkIncomeService;
            _historyReceiveIncomeService = historyReceiveIncomeService;
            _incomeService = incomeService;
            _profitService = profitService;
            _profitComissionService = profitComissionService;
            _profitDailyService = profitDailyService;
            _historyDepositService = historyDepositService;
            _coditionService = coditionService;
            _historyGetIncomeBalanceService = historyGetIncomeBalanceService;
            _historyGetProfitBalanceService = historyGetProfitBalanceService;
            _balanceService = balanceService;

            this._accountService = accountService;
            _accountServiceExtension = new AccountServiceExtension();
        }


        /// GET: Comission
        public ActionResult Index()
        {

            //Get Information of NetworkIncome
            var result = _profitComissionService.GetProfitIncomeByEmail(User.Identity.Name);
            return View(result);
        }


        /// <summary>
        /// NetworkIncome
        /// </summary>
        /// <returns></returns>
        public ActionResult NetworkIncome()
        {
            //Get Information of NetworkIncome
            var result = _networkComissionService.GetNetworkIncomeByEmail(User.Identity.Name);
            return View(result);
        }

        [HttpPost]
        public JsonResult updateStatusNetworkIncomeAndGetIncome(Int32 recordId, bool status, string amount)
        {
            var amountDecimal = decimal.Parse(amount.Replace(".", ",").ToString());
            var recordHistory = _historyNetworkIncomeService.GetHistoryNetworkById(recordId);
            if (recordHistory != null)
            {
                var result = _networkComissionService.UpdateStatusAndUpdateTotalAmountIncome(recordId, status, amountDecimal, User.Identity.Name);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }

        [HttpPost]
        public JsonResult updateStatusReferralAndGetIncome(Int32 recordId, bool status, string amount)
        {
            var amountDecimal = decimal.Parse(amount.Replace(".", ",").ToString());
            var recordHistory = _historyReceiveIncomeService.GetHistoryReceiveIncomeReferralById(recordId);
            if (recordHistory != null)
            {
                var result = _networkComissionService.UpdateStatusReferralIncomeAndUpdateTotalAmountIncome(recordId, status, amountDecimal, User.Identity.Name);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }

        [HttpPost]
        public JsonResult updateStatusReceiveProfit(Int32 recordId, bool status, string amount)
        {
            var amountProfitDaily = decimal.Parse(amount.Replace(".", ",").ToString());
            var recordHistory = _profitDailyService.GetProfitDailyById(recordId);
            if (recordHistory != null)
            {
                var result = _networkComissionService.UpdateStatusReceiveProfit(recordId, status, amountProfitDaily, User.Identity.Name);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }

        [HttpPost]
        public JsonResult insertDepositToData(string amount)
        {
            //Check Level For User and Check Level for Upline
            //Pay Network Commision
            //Pay 10% for Upline
            //Insert Deposit
            if (!string.IsNullOrEmpty(amount) && !(amount == "NaN"))
            {
                var amountP = decimal.Parse(amount.Replace(".", ",").ToString());
                var amountInBalance = _balanceService.GetByEmail(User.Identity.Name).TotalBalance;
                //Check Ltc in balance
                if (amountInBalance >= amountP)
                {
                    var totalDeposit = calTotalDeposit(User.Identity.Name);
                    switch (_accountService.GetByEmail(User.Identity.Name).Level)
                    {
                        case 0:
                            return returnJson(_accountService.GetByEmail(User.Identity.Name).Level, amountP,
                                totalDeposit, _coditionService.Codition_Level_L().MinDeposit, _coditionService.Codition_Level_L().MaxDeposit);
                        case 1:
                            return returnJson(_accountService.GetByEmail(User.Identity.Name).Level, amountP,
                                totalDeposit, _coditionService.Codition_Level_L1().MinDeposit, _coditionService.Codition_Level_L1().MaxDeposit);
                        case 2:
                            return returnJson(_accountService.GetByEmail(User.Identity.Name).Level, amountP,
                               totalDeposit, _coditionService.Codition_Level_L2().MinDeposit, _coditionService.Codition_Level_L2().MaxDeposit);
                        case 3:
                            return returnJson(_accountService.GetByEmail(User.Identity.Name).Level, amountP,
                               totalDeposit, _coditionService.Codition_Level_L3().MinDeposit, _coditionService.Codition_Level_L3().MaxDeposit);
                        case 4:
                            return returnJson(_accountService.GetByEmail(User.Identity.Name).Level, amountP,
                               totalDeposit, _coditionService.Codition_Level_L4().MinDeposit, _coditionService.Codition_Level_L4().MaxDeposit);
                        case 5:
                            return returnJson(_accountService.GetByEmail(User.Identity.Name).Level, amountP,
                               totalDeposit, _coditionService.Codition_Level_L5().MinDeposit, _coditionService.Codition_Level_L5().MaxDeposit);
                        case 6:
                            return returnJson(_accountService.GetByEmail(User.Identity.Name).Level, amountP,
                               totalDeposit, _coditionService.Codition_Level_L6().MinDeposit, _coditionService.Codition_Level_L6().MaxDeposit);
                        case 7:
                            return returnJson(_accountService.GetByEmail(User.Identity.Name).Level, amountP,
                               totalDeposit, _coditionService.Codition_Level_L7().MinDeposit, _coditionService.Codition_Level_L7().MaxDeposit);
                        case 8:
                            return returnJson(_accountService.GetByEmail(User.Identity.Name).Level, amountP,
                               totalDeposit, _coditionService.Codition_Level_L8().MinDeposit, _coditionService.Codition_Level_L8().MaxDeposit);
                        case 9:
                            return returnJson(_accountService.GetByEmail(User.Identity.Name).Level, amountP,
                               totalDeposit, _coditionService.Codition_Level_L9().MinDeposit, _coditionService.Codition_Level_L9().MaxDeposit);
                        case 10:
                            return returnJson(_accountService.GetByEmail(User.Identity.Name).Level, amountP,
                               totalDeposit, _coditionService.Codition_Level_L10().MinDeposit, _coditionService.Codition_Level_L10().MaxDeposit);
                    }
                }
                return Json(new { IsError = false, message = "Fail ! Your balance is not enough ! Please check your balance." });
            }
            return Json(new { IsError = false, message = "Fail ! Please input correct purchase !" });
        }

        [HttpPost]
        public JsonResult getToBalanceNetworkIncome()
        {
            //insert balance
            //Update income = 0
            //Update table get income balance


            //get value in income
            var valIncome = _incomeService.Find(x => x.UserId == User.Identity.Name).SingleOrDefault().TotalAmountIncome;
            //get value balance of user
            var valBalance = _balanceService.GetByEmail(User.Identity.Name).TotalBalance;
            //calculate balance 
            var calculateBalance = valBalance + valIncome;
            if (valIncome != 0)
            {
                //Update Balance               
                int updateBalance = _balanceService.UpdateBalance(User.Identity.Name, calculateBalance);
                //Update Income = 0
                int updateIncomeToZero = _incomeService.UpdateTotalAmountIncomeToZero(User.Identity.Name);
                //Insert to HistoryGetIncomeBalances
                int insertHistoryGetIncomeBalance = _historyGetIncomeBalanceService.InsertHistoryGetIncomeBalance(User.Identity.Name, valIncome);

                return Json(new
                {
                    IsError = true,
                    message = "Successfully"
                });
            }
            return Json(new { IsError = false, message = "Total Amount Income is 0 !" });
        }

        [HttpPost]
        public JsonResult getToBalanceProfitIncome(string amount)
        {
            //insert balance
            //Update table profit = 0
            //Update table get profit balance

            //get value in profit
            var valProfit = _profitService.Find(x => x.UserId == User.Identity.Name).SingleOrDefault().TotalAmountProfit;
            //get value balance of user
            var valBalance = _balanceService.GetByEmail(User.Identity.Name).TotalBalance;
            //calculate balance 
            var calculateBalance = valBalance + valProfit;
            if (valProfit != 0)
            {
                //Update Balance              
                int updateTobalance = _balanceService.UpdateBalance(User.Identity.Name, calculateBalance);
                //Update Profit = 0
                int updateProfitToZero = _profitService.UpdateTotalProfitIncomeToZero(User.Identity.Name);
                //Insert to HistoryGetIncomeBalances
                int insertHistoryGetIncomeBalance = _historyGetProfitBalanceService.InserHistoryGetProfitBalance(User.Identity.Name, valProfit);
                return Json(new
                {
                    IsError = true,
                    message = "Successfully"
                });
            }
            return Json(new { IsError = false, message = "" });
        }


        private decimal calTotalDeposit(string email)
        {
            decimal totalDeposit = 0;
            var list = _historyDepositService.ListHistoryDeposit(email);
            if (list != null)
            {
                foreach (var item in list)
                {
                    totalDeposit += item.Amount;
                }
            }
            return totalDeposit;
        }
        private bool checkCalDeposit(decimal totalDep, decimal amount, decimal max)
        {
            if ((totalDep + amount) <= max) { return true; }
            return false;
        }
        private JsonResult returnJson(int level, decimal amount, decimal totalDep, decimal min, decimal max)
        {
            var minus = max - totalDep;
            if ((amount >= min && amount <= max) || ((max - totalDep + amount) <= max && amount > 0))
            {
                if (checkCalDeposit(totalDep, amount, max))
                {
                    var percent = _coditionService.ReturnPercentProfitDaily(_accountService.GetByEmail(User.Identity.Name).Level);
                    var totalDay = _coditionService.ReturnTotaDayProfitDaily(_accountService.GetByEmail(User.Identity.Name).Level);
                    var result = _historyDepositService.InsertRecordDepositToIncomeDatabase(User.Identity.Name, amount, totalDay, percent);
                    if (result == 1)
                    {
                        //Find Upline
                        var ParentID = _accountService.GetByEmail(User.Identity.Name).ParentId;
                        //handle insert 10% for Upline
                        if (!string.IsNullOrEmpty(ParentID))
                        {
                            var tenPercentOfAmount = (decimal)((amount * 10) / 100);
                            _historyReceiveIncomeService.Insert10PercentForUpline(ParentID, tenPercentOfAmount, User.Identity.Name);
                        }
                        // Check and Update Level for User
                        var checkLevelUser = _accountService.CheckAndUpdateLevel(User.Identity.Name, level);
                        if (_accountService.GetByEmail(User.Identity.Name).Level != checkLevelUser) { _accountService.UpdateLevel(checkLevelUser, User.Identity.Name); }
                        //var checkLevelUser = _accountService.CheckAndUpdateLevel(User.Identity.Name, level);
                        //if (_accountService.GetByEmail(User.Identity.Name).Level != checkLevelUser)
                        //{
                        //    var accLevel = _accountServiceExtension.GetByEmail(User.Identity.Name);
                        //    accLevel.Level = checkLevelUser;
                        //    _accountServiceExtension.Update(accLevel);
                        //}

                        // Check and Update Level for Upline of User
                        var parentLevel = _accountService.GetByEmail(ParentID).Level;
                        var checkLevelUpline = _accountService.CheckAndUpdateLevel(ParentID, parentLevel);
                        if (_accountService.GetByEmail(ParentID).Level != checkLevelUpline) { _accountService.UpdateLevel(checkLevelUpline, User.Identity.Name); }
                        //if (!string.IsNullOrEmpty(ParentID))
                        //{
                        //    var parentLevel = _accountService.GetByEmail(ParentID).Level;
                        //    var checkLevelUpline = _accountService.CheckAndUpdateLevel(ParentID, parentLevel);
                        //    if (_accountService.GetByEmail(ParentID).Level != checkLevelUser)
                        //    {
                        //        var accLevelParent = _accountServiceExtension.GetByEmail(ParentID);
                        //        accLevelParent.Level = checkLevelUser;
                        //        _accountServiceExtension.Update(accLevelParent);
                        //    }
                        //}

                    }
                    return Json(new
                    {
                        IsError = true,
                        message = "Successfully"
                    });
                }
                return Json(new
                {
                    IsError = false,
                    message = "Your Level is L" + level + ". Total escrow is : " + (float)max + " LTC." + " You only can deposit " + (float)minus + " LTC. Because you have escrowed : " + (float)totalDep + " LTC."
                });
            }
            return Json(new
            {
                IsError = false,
                message = "Your Level is L" + level + ". You only can deposit from " + (float)(min)
                + " LTC to " + (float)max + " LTC. And your remain escrow is " + (float)minus + " LTC. Because you have escrowed : " + (float)totalDep + " LTC."
            });

        }

    }
}

