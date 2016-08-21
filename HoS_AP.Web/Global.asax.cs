using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HoS_AP.DI;

namespace HoS_AP.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapMvcAttributeRoutes();

            var container = new InversionOfControlContainer();
            container.RegisterControllers(
                Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.BaseType == typeof(Controller))
            );

            ControllerBuilder.Current.SetControllerFactory(new MyControllerFactory(container));
        }
    }
}