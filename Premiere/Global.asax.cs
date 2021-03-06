﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using Premiere.Models;
using System.Text;
using Premiere.App_Start;
using System.Web.Security;
using Infrustructure;

namespace Premiere
{

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "ChartsRoute",
            url: "Charts/WebSite/{*pathinfo}"
        );
        }


        protected void Application_Start()
        {
            
            //Database.SetInitializer<Premiere.Models.PremiereDB>(null);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BackloadConfig.Initialize();
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            Infrustructure.UtilFunction util = new UtilFunction();

            //util.AddMonthDate();
            
            //Membership.GetUser("robin521").UnlockUser();
            //string newpsw=Membership.GetUser("robin521").ResetPassword();
            //Membership.GetUser("robin521").ChangePassword(newpsw, "Baidu123");

        }

    }
}