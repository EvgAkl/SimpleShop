using System.Web.Mvc;
using System.Web.Routing;
using SimpleShop.Domain.Entities;
using SimpleShop.WebUI.Infrastructure.Binders;

namespace SimpleShop.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
