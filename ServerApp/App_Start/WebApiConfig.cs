using System.Web.Http;

namespace ServerApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "GetCity",
                routeTemplate: "api/{controller}/{cityId}",
                defaults: new { cityId = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "GetCityList",
                routeTemplate: "api/{controller}/search/{searchTerm}",
                defaults: new { searchTerm = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "GetEvent",
                routeTemplate: "api/{controller}/{cityId}/{eventTime}"
            );

        }
    }
}
