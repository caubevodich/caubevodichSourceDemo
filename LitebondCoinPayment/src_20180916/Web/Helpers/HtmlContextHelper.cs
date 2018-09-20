using System.Linq;
using System.Security.Claims;
using System.Web;

namespace WebUI.Helpers
{
    public static class HtmlContextHelper
    {
        public static string GetFullName(this HttpContextBase context)
        {
            ClaimsIdentity claimsIdentity = context.User.Identity as ClaimsIdentity;          
            string fullName =
                claimsIdentity?.Claims.Where(p => p.Type == ClaimTypes.Surname).Select(p => p.Value).FirstOrDefault();

            return fullName;
        }

        public static string GetAvatar(this HttpContext context)
        {
            ClaimsIdentity claimsIdentity = context.User.Identity as ClaimsIdentity;

            string avartar =
                claimsIdentity?.Claims.Where(p => p.Type == ClaimTypes.GivenName).Select(p => p.Value).FirstOrDefault();

            return avartar;
        }
    }
}