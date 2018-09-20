using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string referral)
        {
            if (!string.IsNullOrWhiteSpace(referral))
            {
                Response.Cookies["ReferralLink"].Value = referral;
                return RedirectToAction("Register", "Account", new { Referral = referral });
            }
            return View();
        }

        [Route("gioi-thieu/ve-chung-toi.html")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}