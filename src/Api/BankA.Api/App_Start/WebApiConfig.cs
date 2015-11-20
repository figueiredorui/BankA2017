using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BankA.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Type t1 = typeof(BankA.Controllers.Controllers.HomeController);
            Type t2 = typeof(BankA.Controllers.Controllers.AccountsController);
            Type t3 = typeof(BankA.Controllers.Controllers.ReportsController);
            Type t4 = typeof(BankA.Controllers.Controllers.TransactionsController);
            Type t5 = typeof(BankA.Controllers.Controllers.RulesController);


            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApie",
                routeTemplate: "",
                //routeTemplate: "{controller}/{id}",
                defaults: new { controller = "Home", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                //routeTemplate: "{controller}/{id}",
                defaults: new { controller = "Home", id = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //"swagger_root",
            //"",
            //null,
            //null,
            //new RedirectHandler("swagger/ui/index.html"));


            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new MyJsonDateTimeConverter());

            //var serializerSettings = new JsonSerializerSettings();
            //serializerSettings.Converters.Add(new IsoDateTimeConverter());
            //GlobalConfiguration.Configuration.Formatters[0] = new JsonNetFormatter(serializerSettings);
        }
    }

    public class MyJsonDateTimeConverter : IsoDateTimeConverter

    {

        public MyJsonDateTimeConverter()

        {

            base.DateTimeFormat = "yyyy-MM-dd";

        }

    }
}
