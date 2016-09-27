namespace HoS_AP.Web.Filters
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;

    public class LanguageAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string cultureName = null;
            
            HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];

            cultureName = cultureCookie != null ? cultureCookie.Value : "en";

            // cultures list
            List<string> cultures = new List<string> { "ru", "en" };
            if (!cultures.Contains(cultureName))
            {
                cultureName = "en";
            }

            var culture = CultureInfo.CreateSpecificCulture(cultureName);

            CultureInfo.DefaultThreadCurrentUICulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}