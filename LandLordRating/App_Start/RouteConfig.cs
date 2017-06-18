using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LandLordRating
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "LandLord",
                "LandLords/{action}",
                new {controller = "LandLords", action = "Index"}
            );

            routes.MapRoute(
                "Admin",
                "Admin/{action}",
                new { controller = "Admin", action = "Index" }
            );

            routes.MapRoute(
                "Manage",
                "Manage/{action}",
                new { controller = "Manage", action = "Index" }
            );

            routes.MapRoute(
                "Account",
                "Account/{action}",
                new { controller = "Account", action = "Index" }
            );

            routes.MapRoute(
                "HomeAction",
                "{action}",
                new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
