using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;

namespace BankSystem
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            //Allow access to other host to get and produces data like connect with Angular 
            config.EnableCors(new EnableCorsAttribute("http://localhost:4200", headers: "*", methods: "*"));
           // GlobalConfiguration.Configure(WebApiConfig.Register);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
