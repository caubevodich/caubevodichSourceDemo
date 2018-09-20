using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Core.Domain.Entities;
using Core.Helper;
using Core.Services;
using Google.Authenticator;
using Microsoft.AspNet.Identity;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class MeController : Controller
    {
        private readonly IEntityService<LoginHistory> _loginHistoryService;

        private readonly IAccountService _accountService;

        public MeController(IEntityService<LoginHistory> loginHistoryService, IAccountService accountService)
        {
            _loginHistoryService = loginHistoryService;
            _accountService = accountService;
        }

        public async Task<ActionResult> Index()
        {
            var userId = ConvertType.ToInt(User.Identity.GetUserId());
            var currentUser = _accountService.Get(userId);

            string referralLink = "";

            if (!string.IsNullOrWhiteSpace(currentUser.ReferralLink))
            {
                referralLink = currentUser.ReferralLink;
            }
            else
            {
                referralLink = Helpers.Helpers.GetReferralLinkFromEmail(currentUser.Email);
                currentUser.ReferralLink = referralLink;
                _accountService.Update(currentUser);
            }

            referralLink = "http://litebond.co/?referral=" + referralLink;

            var profileViewModel = new ProfileViewModel
            {
                TwoFactorEnabled = currentUser.TwoFactorEnabled,
                ReferralId = referralLink,
                Email = currentUser.Email,
                FullName = currentUser.FullName,
                PhoneNumber = currentUser.PhoneNumber,
                WalletAddress = currentUser.WalletAddress
            };
            if (!currentUser.TwoFactorEnabled)
            {
                var authencator = new TwoFactorAuthenticator(true, true);
                //string authenKey = currentUser.PasswordHash.Substring(0, 10);
                string authenKey = currentUser.Email;
                var authenSetup = authencator.GenerateSetupCode("Litebond.co - " + currentUser.Email, authenKey, 300, 300);
                profileViewModel.ManualEntryKey = authenSetup.ManualEntryKey;
                profileViewModel.QrCodeSetupImageUrl = authenSetup.QrCodeSetupImageUrl;
            }

            var logs = _loginHistoryService.Find(x => x.UserId == userId).OrderByDescending(x => x.Id).Take(10).ToList();
            profileViewModel.LoginHistorys = logs;
            return View(profileViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnabledTwoFactor(string TwoFactorCode)
        {
            var userId = ConvertType.ToInt(User.Identity.GetUserId());
            var currentUser = _accountService.Get(userId);

            var notification = new Notification
            {
                IsSuccess = false
            };
            var authencator = new TwoFactorAuthenticator(true, true);
            // string authenKey = currentUser.PasswordHash.Substring(0, 10);
            string authenKey = currentUser.Email;
            var isValid = authencator.ValidateTwoFactorPIN(authenKey, TwoFactorCode);
            if (isValid)
            {
                currentUser.TwoFactorEnabled = true;
                _accountService.Update(currentUser);

                notification.IsSuccess = true;
                notification.Message = " Enabled 2FA";
                TempData["Notification"] = notification;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisabledTwoFactor(string TwoFactorCode, string Password)
        {
            var notification = new Notification
            {
                IsSuccess = false
            };
            var userId = ConvertType.ToInt(User.Identity.GetUserId());
            var currentUser = _accountService.Get(userId);
            if (currentUser != null)
            {
                var hasPassword = Password.EncryptSHA256();
                if (currentUser.PasswordHash == hasPassword)
                {
                    var authencator = new TwoFactorAuthenticator(true, true);
                    //string authenKey = currentUser.PasswordHash.Substring(0, 10);
                    string authenKey = currentUser.Email;
                    var isValid = authencator.ValidateTwoFactorPIN(authenKey, TwoFactorCode);

                    if (isValid)
                    {
                        currentUser.TwoFactorEnabled = false;
                        _accountService.Update(currentUser);
                        notification.IsSuccess = true;
                        notification.Message = "Disabled 2FA";
                        TempData["Notification"] = notification;
                    }
                    else
                    {

                        notification.Message = "Error 2FA";
                        TempData["Notification"] = notification;
                    }
                }
                else
                {
                    notification.Message = "Error Password";
                    TempData["Notification"] = notification;
                }
            }
         
          
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile(ProfileViewModel model)
        {
            var notification = new Notification
            {
                IsSuccess = false
            };
            if (!ModelState.IsValid)
            {
                notification.Message = "Cannot change profile";

                TempData["Notification"] = notification;

                return RedirectToAction("Index");
            }
            var account = _accountService.GetByEmail(model.Email);
            if (account != null)
            {
                account.FullName = model.FullName;
                account.PhoneNumber = model.PhoneNumber;
                _accountService.Update(account);

                notification.IsSuccess = true;
                notification.Message = "Successful";
                TempData["Notification"] = notification;
                return RedirectToAction("Index", new { Message = "Successful" });
            }
            else
            {
                notification.Message = "Cannot change profile";
                TempData["Notification"] = notification;
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(string OldPassword, string NewPassword, string two2fapass)
        {
            var notification = new Notification
            {
                IsSuccess = false
            };
            if (!ModelState.IsValid)
            {
                notification.Message = "Cannot change password";

                TempData["Notification"] = notification;

                return RedirectToAction("Index");
            }
            if (string.IsNullOrWhiteSpace(OldPassword) && string.IsNullOrWhiteSpace(NewPassword))
            {
                notification.Message = "Cannot change password";

                TempData["Notification"] = notification;

                return RedirectToAction("Index");
            }
            var userId = ConvertType.ToInt(User.Identity.GetUserId());
            var currentUser = _accountService.Get(userId);
            if (currentUser.TwoFactorEnabled)
            {
                var authencator = new TwoFactorAuthenticator(true, true);
                //string authenKey = currentUser.PasswordHash.Substring(0, 10);
                string authenKey = currentUser.Email;
                var isValid = authencator.ValidateTwoFactorPIN(authenKey, two2fapass);
                if (isValid)
                {
                    var result = _accountService.ChangePassword(User.Identity.Name, OldPassword, NewPassword);
                    if (result.Code == 200)
                    {
                        notification.IsSuccess = true;
                        notification.Message = "Successful";
                        TempData["Notification"] = notification;
                        return RedirectToAction("Index", new { Message = "Successful" });
                    }
                    else
                    {
                        notification.Message = "Cannot change your password";
                    }
                }
                else
                {
                    notification.Message = "2FA Error";
                    TempData["Notification"] = notification;
                }
            }
            else
            {
                var result = _accountService.ChangePassword(User.Identity.Name, OldPassword, NewPassword);
                if (result.Code == 200)
                {
                    notification.IsSuccess = true;
                    notification.Message = "Successful";
                    TempData["Notification"] = notification;
                    return RedirectToAction("Index", new { Message = "Successful" });
                }
                else
                {
                    notification.Message = "Cannot change your password";
                }
            }
            return RedirectToAction("Index");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeWalletAddress(string addresslite, string two2faAddress)
        {
            var notification = new Notification
            {
                IsSuccess = false
            };
            if (!ModelState.IsValid)
            {
                notification.Message = "Cannot change address";

                TempData["Notification"] = notification;

                return RedirectToAction("Index");
            }

            var userId = ConvertType.ToInt(User.Identity.GetUserId());
            var currentUser = _accountService.Get(userId);
            if (currentUser.TwoFactorEnabled)
            {
                var authencator = new TwoFactorAuthenticator(true, true);
                //string authenKey = currentUser.PasswordHash.Substring(0, 10);
                string authenKey = currentUser.Email;
                var isValid = authencator.ValidateTwoFactorPIN(authenKey, two2faAddress);
                if (isValid)
                {
                    var result = _accountService.ChangeAddressLitecoin(User.Identity.Name, addresslite);
                    if (result.Code == 200)
                    {
                        notification.IsSuccess = true;
                        notification.Message = "Successful";
                        TempData["Notification"] = notification;
                        return RedirectToAction("Index", new { Message = "Successful" });
                    }
                    else
                    {
                        notification.Message = "Cannot change your LTC Wallet Address";
                    }
                }
                else
                {
                    notification.Message = "2FA Error";
                    TempData["Notification"] = notification;
                }
            }
            return RedirectToAction("Index");
        }


    }
}