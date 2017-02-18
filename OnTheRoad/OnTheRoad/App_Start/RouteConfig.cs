using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace OnTheRoad
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            RegisterParameterRoutes(routes);

            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);
        }

        private static void RegisterParameterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(
                "CategoriesRoute",
                "Categories/{categoryName}/",
                "~/Categories.aspx"
            );

            routes.MapPageRoute(
                "TripsRoute",
                "Trips/{tripId}/",
                "~/Trips/Default.aspx"
                );

            routes.MapPageRoute(
                "TripsSearchRoute",
                "Trips/Search/{pattern}/",
                "~/Trips/Default.aspx"
                );
        }
    }
}