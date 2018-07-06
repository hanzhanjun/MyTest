using SqlHelpCore;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Startup.Configuration();
            AreaRegistration.RegisterAllAreas();           
            RouteConfig.RegisterRoutes(RouteTable.Routes);           
        }
    }
}
