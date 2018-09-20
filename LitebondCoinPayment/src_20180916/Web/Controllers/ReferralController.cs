using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Core.Domain.Models;
using Core.Services;

namespace WebUI.Controllers
{
    [Authorize]
    public class ReferralController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IHistoryDepositService _historyDepositService;

        public ReferralController(IAccountService accountService, IHistoryDepositService historyDepositService)
        {
            _accountService = accountService;
            _historyDepositService = historyDepositService;
        }

        // GET: Referral
        public ActionResult Index()
        {
            var litsAccounts = _accountService.ListReferrals(User.Identity.Name);
            var accountInfo = _accountService.GetByEmail(User.Identity.Name);
            var referrals = new List<ReferralModel>();
            var now = DateTime.UtcNow;
            var reflink = "";

            if (accountInfo != null)
            {
                if (!string.IsNullOrWhiteSpace(accountInfo.ReferralLink)) { reflink = accountInfo.ReferralLink; }
                else
                {
                    reflink = Helpers.Helpers.GetReferralLinkFromEmail(accountInfo.Email);                
                   
                }

            }
            if (!string.IsNullOrWhiteSpace(reflink))
            {
                reflink = "https://litebond.co/?referral=" + reflink;
            }

            foreach (var item in litsAccounts)
            {
                referrals.Add(new ReferralModel
                {
                    CreatedAt = now,
                    Email = item.Email,
                    Level = item.Level,
                    Status = "Approved",
                    TotalDeposit = _historyDepositService.calTotalDeposit(item.Email)
                });
            }

            var refTotalSales = _accountService.totalDepOfTotalNetworkNoUseGetNetWorkComission(User.Identity.Name);
            var refTotalRefOfNetwork = _accountService.RecursiveOfUser(User.Identity.Name).Count;
            var refModel = new RefModel();
            refModel.refLink = reflink;
            refModel.RefList = referrals;
            refModel.TotalSales = refTotalSales;
            refModel.TotalRefOfNetwork = refTotalRefOfNetwork;
            return View(refModel);
        }
    }
}