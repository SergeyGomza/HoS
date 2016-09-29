using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoS_AP.Web.Modules
{
    using System.Globalization;
    using System.Threading;

    public class CultureModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.BeginRequest += HandleBeginRequest;
        }
        private void HandleBeginRequest(object src, EventArgs args)
        {
            var cultureCookie = HttpContext.Current.Request.Cookies["lang"];
            var cultureName = cultureCookie != null ? cultureCookie.Value : "en";

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

        public void Dispose()
        {}
    }
}