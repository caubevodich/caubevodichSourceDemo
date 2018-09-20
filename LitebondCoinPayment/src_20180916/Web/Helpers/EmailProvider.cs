using System.Collections.Generic;
using System.Linq;
using ColorLife.Core.Helper;

namespace WebUI.Helpers
{
    public abstract class EmailBasic
    {
        private readonly string _subject;

        private readonly string[] _toEmailRange;

        protected EmailBasic(string subject, params string[] toEmail)
        {
            _subject = subject;
            _toEmailRange = toEmail;
        }
    }

    public class EmailProvider : EmailBasic, IEmailProvider
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _subject;
        private readonly string _siteLogo;
        private readonly string _siteUrl;
        private readonly string[] _toEmailRange;
        private readonly string[] _toCCEmailRange;
        private readonly string[] _toBCCEmailRange;
        private readonly string[] _fileAttachments;
        private readonly string _fromName;
       

        // with CRM

        public EmailProvider(string subject, params string[] toEmail) : base(subject, toEmail)
        {
            _toEmailRange = toEmail;

            _smtpHost = "smtp.gmail.com";

            _smtpPort = 587;
            // _smtpUsername = "tuanitpro@gmail.com";
            // _smtpPassword = "xcljsbbeuepcgcpl";
            _smtpUsername = "info.litebond@gmail.com";
            _smtpPassword = "xuthedautu";
            _subject = subject;

            _siteLogo = "https://www.zappbug.com/wp-content/uploads/2015/05/twitter-logo-small.png";
            //  _siteUrl = SettingOption.Get("SITE_URL");// "http://localhost";
            // optional config here
        }

        public bool Send(string bodyTemplate, Dictionary<string, string> paras)
        {
            if (string.IsNullOrEmpty(bodyTemplate))
                return false;

            string body = bodyTemplate;
            body = body.Replace("[siteUrl]", _siteUrl);
            body = body.Replace("[siteLogo]", _siteLogo);
            if (paras != null && paras.Any())
            {
                foreach (var item in paras)
                {
                    body = body.Replace(item.Key, item.Value);
                }
            }

            IColorMailService mailService = new ColorMailService
            {
                SmtpHost = _smtpHost,
                SmtpPort = _smtpPort,
                SmtpUserName = _smtpUsername,
                SmtpPassword = _smtpPassword,

                FromEmail = _smtpUsername,

                EnableSsl = true,
                UseDefaultCredentials = true,

                ToEmail = _toEmailRange,
                ToCcEmail = _toCCEmailRange,
                ToBccEmail = _toBCCEmailRange,
                FromName = _fromName,
                FileAttachments = _fileAttachments,
                Subject = _subject,
                Body = body
            };
            return mailService.Send();
        }
    }
}