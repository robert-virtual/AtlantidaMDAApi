using AtlantidaMDAApi.App_Start;
using AtlantidaMDAApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace AtlantidaMDAApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API

            config.EnableCors();
            // Rutas de Web API
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new JwtValidationHandler()); 

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Add(new ReturnJsons());
            config.Formatters.Add(new TextMediaTypeFormatter());
        }
    }
}
