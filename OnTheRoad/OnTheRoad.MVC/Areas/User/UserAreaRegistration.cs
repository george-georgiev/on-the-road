using System.Web.Mvc;

namespace OnTheRoad.MVC.Areas.User
{
    public class UserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "UserFollow",
                "User/Profile/Follow/{username}",
                new { controller = "Profile", action = "Follow", username = UrlParameter.Optional }
            );

            context.MapRoute(
                "UserUnfollow",
                "User/Profile/Unfollow/{username}",
                new { controller = "Profile", action = "Unfollow", username = UrlParameter.Optional }
            );

            context.MapRoute(
                "UserUpdate",
                "User/Profile/Update/{username}",
                new { controller = "Profile", action = "Update", username = UrlParameter.Optional }
            );

            context.MapRoute(
                "UserProfile",
                "User/Profile/{username}",
                new { controller = "Profile", action = "Index", username = UrlParameter.Optional }
            );

            context.MapRoute(
                "UserAddReview",
                "User/Reviews/Add",
                new { controller = "Reviews", action = "Add" }
            );

            context.MapRoute(
                "UserReviews",
                "User/Reviews/{username}/{page}",
                new { controller = "Reviews", action = "Index", username = UrlParameter.Optional, page = UrlParameter.Optional }
            );

            context.MapRoute(
                "UserConversations",
                "User/Conversations/{username}",
                new { controller = "Messages", action = "Conversations", username = UrlParameter.Optional }
            );

            context.MapRoute(
                "UserMessages",
                "User/Messages/{username}",
                new { controller = "Messages", action = "Index", username = UrlParameter.Optional }
            );

            context.MapRoute(
                "User_default",
                "User/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}