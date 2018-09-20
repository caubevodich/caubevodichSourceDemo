using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Core.Helper
{
    public interface IColorMailService
    {
        bool Send();

        string SendWithEx();

        bool Send(string toEmail, string subject, string body, string smtpUserName, string smtpPassword, string smtpHost, int smtpPort);
    }

    public class ColorMailService : IColorMailService
    {
        public MailMessage MailMessage { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool EnableSsl { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ReplyToEmail { get; set; }
        public string[] ToEmail { get; set; }
        public string[] ToCcEmail { get; set; }
        public string[] ToBccEmail;

        public string[] FileAttachments { get; set; }

        public bool Send()
        {
            string rs = SendWithEx();
            return rs == string.Empty;
        }

        public bool Send(string toEmail, string subject, string body, string smtpUserName, string smtpPassword, string smtpHost, int smtpPort)
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Host = smtpHost;
                    smtpClient.Port = smtpPort;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                    var msg = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(smtpUserName),
                        Subject = subject,
                        Body = body,
                        Priority = MailPriority.Normal,
                    };
                    //  msg.ReplyToList.Add(new MailAddress("tuanitpro@gmail.com"));
                    msg.ReplyToList.Add(new MailAddress("info.litebond@gmail.com"));
                    msg.To.Add(toEmail);

                    smtpClient.Send(msg);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public string SendWithEx()
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = this.EnableSsl;
                    smtpClient.Host = this.SmtpHost;
                    smtpClient.Port = this.SmtpPort;
                    smtpClient.UseDefaultCredentials = this.UseDefaultCredentials;
                    smtpClient.Credentials = new NetworkCredential(this.SmtpUserName, this.SmtpPassword);
                    var msg = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(FromEmail, FromName),
                        Subject = this.Subject,
                        Body = this.Body,
                        Priority = MailPriority.Normal,
                        Sender = new MailAddress(FromEmail),
                    };
                    foreach (var item in this.ToEmail)
                    {
                        msg.To.Add(new MailAddress(item));
                    }
                    if (ToCcEmail != null && ToCcEmail.Any())
                    {
                        foreach (var item in ToCcEmail)
                        {
                            msg.CC.Add(new MailAddress(item));
                        }
                    }
                    if (ToBccEmail != null && ToBccEmail.Any())
                    {
                        foreach (var item in ToBccEmail)
                        {
                            msg.Bcc.Add(new MailAddress(item));
                        }
                    }
                    if (FileAttachments != null && FileAttachments.Any())
                    {
                        foreach (var item in FileAttachments)
                        {
                            var attach = new Attachment(item);
                            msg.Attachments.Add(attach);
                        }
                    }

                    smtpClient.Send(msg);
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}