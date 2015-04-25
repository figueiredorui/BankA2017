﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BankA.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Type t2 = typeof(BankA.Api.Controllers.AccountsController);
            Type t3 = typeof(BankA.Api.Controllers.ReportsController);
            Type t4 = typeof(BankA.Api.Controllers.TransactionsController);


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
        }
    }
}
