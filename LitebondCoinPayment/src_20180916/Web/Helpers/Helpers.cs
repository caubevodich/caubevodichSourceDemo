using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace WebUI.Helpers
{
    public static class Helpers
    {
        public static string GetReferralLinkFromEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return string.Empty;
            MailAddress addr = new MailAddress(email);
            string username = addr.User;
            return username;
        }
    }
}