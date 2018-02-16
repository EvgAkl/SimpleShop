using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleShop.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Show full pages full categories
            routes.MapRoute(
                name: null,
                url: "",
                defaults: new { controller = "Game", action = "List", category = (string)null, page = 1 }
                );

            // Transition by pages
            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { controller = "Game", action = "List", category = (string)null },
                constraints: new { page = @"\d+" }
                );

            // Transition by categories
            routes.MapRoute(
                name: null,
                url: "{category}",
                defaults: new { controller = "Game", action = "List"}
                );

            // Transition by category pages
            routes.MapRoute(
                name: null,
                url: "{category}/Page{page}",
                defaults: new { controller = "Game", action = "List" },
                constraints: new { page = @"\d+" }
                );

            // Default
            routes.MapRoute(
                name: null,
                url: "{controller}/{action}"
                );

        } // end RegisterRoutes()

    } // end class
} // end namespace
