using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SharpCMS.UI.Mvc.Infrastructure;

namespace SharpCMS.UI.Mvc
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UnderConstAttribute());
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Articles", "article/{action}/{type}/{id}", new {controller = "Article"});

            routes.MapRoute(
                "Default", // Имя маршрута
                "{controller}/{action}/{id}", // URL-адрес с параметрами
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Параметры по умолчанию
                );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);

            RegisterRoutes(RouteTable.Routes);
        }
    }
}