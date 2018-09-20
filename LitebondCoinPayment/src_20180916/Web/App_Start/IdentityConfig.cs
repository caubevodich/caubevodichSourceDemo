using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Twilio;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using WebUI.Models;

namespace WebUI
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return configSendGridasync(message);
            // Plug in your email service here to send an email.

            //return Task.FromResult(0);
        }

        private async Task configSendGridasync(IdentityMessage message)
        {
            var smtp = new SmtpClient("smtp.gmail.com", 587);

            //  var creds = new NetworkCredential("tuanitpro@gmail.com", "xcljsbbeuepcgcpl");
            var creds = new NetworkCredential("info.litebond@gmail.com", "xuthedautu");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = creds;
            smtp.EnableSsl = true;

            var msg = new MailMessage();

            msg.To.Add(new MailAddress(message.Destination));
            //  msg.From = new MailAddress("tuanitpro@gmail.com", "BTC");
            msg.From = new MailAddress("info.litebond@gmail.com", "BTC");
            msg.Subject = message.Subject;
            msg.Body = message.Body;
            msg.IsBodyHtml = true;
            //  using (var smtp = new SmtpClient())
            //{
            await smtp.SendMailAsync(msg);
            // }
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage msg)
        {
            // Plug in your SMS service here to send a text message.
            // Find your Account Sid and Auth Token at twilio.com/user/account
            string accountSid = ConfigurationManager.AppSettings["AccountSid"];
            string authToken = ConfigurationManager.AppSettings["AuthToken"];
            string SMSAccountFrom = ConfigurationManager.AppSettings["SMSAccountFrom"];

            var twilio = new TwilioRestClient(accountSid, authToken);

            // Twilio Begin

            // var result = twilio..SendMessage(SMSAccountFrom, message.Destination, message.Body);

            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber(msg.Destination);
            var message = MessageResource.Create(
                to,
                from: new PhoneNumber(SMSAccountFrom),
                body: msg.Body);

            // Status is one of Queued, Sending, Sent, Failed or null if the number is not valid
            // Trace.TraceInformation(result.Status);
            // Twilio doesn't currently have an async API, so return success.
            // return Task.FromResult(0);
            // Twilio End

            // ASPSMS Begin
            //var soapSms = new ASPSMSX2.ASPSMSX2SoapClient("ASPSMSX2Soap");
            //soapSms.SendSimpleTextSMS(
            //    System.Configuration.ConfigurationManager.AppSettings["ASPSMSUSERKEY"],
            //    System.Configuration.ConfigurationManager.AppSettings["ASPSMSPASSWORD"],
            //    message.Destination,
            //    System.Configuration.ConfigurationManager.AppSettings["ASPSMSORIGINATOR"],
            //    message.Body);
            //soapSms.Close();
            // ASPSMS End

            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords

            //manager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = false,
            //    RequireDigit = false,
            //    RequireLowercase = false,
            //    RequireUppercase = false,
            //};

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(RoleStore<IdentityRole> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        }
    }
}