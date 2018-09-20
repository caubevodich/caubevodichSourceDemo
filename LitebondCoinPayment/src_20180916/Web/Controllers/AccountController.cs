using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Core.Domain.Entities;
using Core.Helper;
using Core.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using StackExchange.Redis;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IEntityService<LoginHistory> _loginHistoryService;

        private readonly IAccountService _accountService;

        public AccountController(IEntityService<LoginHistory> loginHistoryService, IAccountService accountService)
        {
            _loginHistoryService = loginHistoryService;
            this._accountService = accountService;
        }

        [AllowAnonymous]
        public ActionResult Index() => RedirectToAction("Login");

        [AllowAnonymous]       
        public ActionResult Login(string returnUrl)
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    AuthenticationManager.SignOut();
            //}
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private void SignIn(Account rs, bool remember)
        {
            var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, rs.Email ??rs.Id.ToString() ),
                        new Claim(ClaimTypes.GivenName, rs.FullName??rs.Email)   ,
                        new Claim(ClaimTypes.NameIdentifier, rs.Id.ToString())   ,
                         new Claim(ClaimTypes.Sid, rs.Id.ToString())   ,
                            new Claim(ClaimTypes.Thumbprint, rs.Avatar??string.Empty)   ,
                         }, DefaultAuthenticationTypes.ApplicationCookie);

            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            AuthenticationProperties auth = new AuthenticationProperties();
            auth.AllowRefresh = true;
            auth.IsPersistent = remember;
            if (auth.IsPersistent)
                auth.ExpiresUtc = DateTime.UtcNow.AddDays(7);

            authManager.SignIn(auth, identity);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]        
        public async Task<ActionResult> Login(LoginViewModel model, bool CaptchaValid)
        {
            if (!CaptchaValid)
            {
                ModelState.AddModelError("", "Invalid reCaptcha");
                return View(model);
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = _accountService.Login(model.Email, model.Password);
            if (result.Data != null && result.Data is Account)
            {
                var account = (Account)result.Data;
                SignIn(account, model.RememberMe);
                // var userId = User.Identity.GetUserId(); // .RequestContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var loginHistory = new LoginHistory
                {
                    UserAgent = Request.UserAgent.ToString(),
                    IPAddress = Request.UserHostAddress,
                    LoginTime = DateTime.UtcNow,
                    UserId = account.Id
                };
                _loginHistoryService.Insert(loginHistory);

                return RedirectToAction("Index", "Dashboard");
            }           
            ModelState.AddModelError("", result.Message);
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register(string referral)
        {
            if (!string.IsNullOrWhiteSpace(referral))
            {
                Response.Cookies["ReferralLink"].Value = referral;
            }

            var model = new RegisterViewModel
            {
                ReferralLink = referral
            };

            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, bool CaptchaValid)
        {
            if (!CaptchaValid)
            {
                ModelState.AddModelError("", "Invalid reCaptcha");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                string referralLink = Helpers.Helpers.GetReferralLinkFromEmail(model.Email);
                string getReferralLink = model.ReferralLink;
                string emailOfParent = "";
                var user = new Account
                {
                    UserName = model.Email,
                    Email = model.Email,
                    ReferralLink = referralLink,
                    EmailConfirmed = true,
                    PasswordHash = model.Password
                };
                if (string.IsNullOrWhiteSpace(getReferralLink))
                {
                    getReferralLink = Request.Cookies["ReferralLink"] == null ? string.Empty : Request.Cookies["ReferralLink"].Value;
                }
                if (!string.IsNullOrWhiteSpace(getReferralLink))
                {
                    var parentUser = _accountService.Find(x => x.ReferralLink == getReferralLink).FirstOrDefault();
                    if (parentUser != null)
                    {
                        user.ParentId = parentUser.Email;
                    }
                    // user.ParentId
                }

                var result = _accountService.Register(user);
                if (result.Code == 200)
                {
                    if (Request.Cookies["ReferralLink"] != null)
                    {
                        Request.Cookies["ReferralLink"].Expires = DateTime.Now.AddDays(-1);
                    }
                    IEmailProvider _provider = new EmailProvider("Register successful", model.Email);

                    string loginUrl = $"https://litebond.co/Account/Login";
                    string body = "Hello [yourUsername], Thank for register. Clicking link below to login<br/>[loginUrl]";
                    Dictionary<string, string> dics = new Dictionary<string, string> {
                        { "[yourUsername]", model.Email },
                        {"[loginUrl]", loginUrl }
                    };
                    _provider.Send(body, dics);

                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Cannot register. Please try again");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        //[AllowAnonymous]
        //public async Task<ActionResult> ConfirmEmail(string userId, string code)
        //{
        //    if (userId == null || code == null)
        //    {
        //        return View("Error");
        //    }
        //    var result = await UserManager.ConfirmEmailAsync(userId, code);
        //    return View(result.Succeeded ? "ConfirmEmail" : "Error");
        //}

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = _accountService.CreateActionCode(model.Email);
                if (account == null)
                {
                    ModelState.AddModelError("", "User not found");
                    return View(model);
                }
                IEmailProvider _provider = new EmailProvider("Reset Your Password", model.Email);

                string resetUrl = $"https://litebond.co/Account/ResetPassword?code=" + account.VerificationCode;
                string body = "Hello [yourUsername], Please reset your password by clicking link below<br/>[resetUrl]";
                Dictionary<string, string> dics = new Dictionary<string, string> {
                        { "[yourUsername]", account.Email },
                        {"[resetUrl]", resetUrl }
                    };
                _provider.Send(body, dics);

                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (code == null)
                return RedirectToAction("ForgotPassword");
            var account = _accountService.GetByVerificationCode(code);
            if (account == null)
                return RedirectToAction("ForgotPassword");
            var model = new ResetPasswordViewModel
            {
                Email = account.Email
            };
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Code))
                    return RedirectToAction("Index");
                if (model.Code.Length > 40) return RedirectToAction("Index");

                var account = _accountService.GetByVerificationCode(model.Code);
                if (account == null)
                    return RedirectToAction("ResetPassword", new { id = model.Code });

                var result = _accountService.ResetPassword(model.Code, model.Email, model.Password);
                if (result.Code == 200)
                {
                    string body = "Hello [yourUsername],";

                    IEmailProvider _provider = new EmailProvider(account.Email, "Your Password Has Been Changed");

                    Dictionary<string, string> dics = new Dictionary<string, string> {
                        { "[yourUsername]", model.Email },
                    };
                    _provider.Send(body, dics);

                    return RedirectToAction("Index");
                }

                TempData["Notification"] = result.Message;
                return RedirectToAction("ResetPassword", new { id = model.Code });
                // Reset code
            }

            TempData["Notification"] = "Error, please try again.";
            return RedirectToAction("ResetPassword", new { id = model.Code });
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
           
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            var ctx = Request.GetOwinContext();
            ctx.Authentication.SignOut();            
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion Helpers
    }
}