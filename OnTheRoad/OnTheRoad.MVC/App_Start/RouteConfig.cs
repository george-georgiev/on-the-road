using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnTheRoad.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CategoryByName",
                url: "Categories/Details/{categoryName}/{page}",
                defaults: new { controller = "Categories", action = "Details", categoryName = UrlParameter.Optional, page = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TripsSearch",
                url: "Trips/Search/{pattern}/{page}",
                defaults: new { controller = "Trips", action = "Search", pattern = UrlParameter.Optional, page = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
