using System.Web.Mvc;
using WebUI.Filters;

namespace WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new RecaptchaFilterAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}