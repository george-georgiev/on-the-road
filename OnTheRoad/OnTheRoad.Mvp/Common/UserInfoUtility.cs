using System.Security.Claims;
using System.Security.Principal;

namespace OnTheRoad.Mvp.Common
{
    public static class UserInfoUtility
    {
        public static string GetCurrentUserId(this IIdentity identity)
        {
            var result = ((ClaimsIdentity)identity).FindFirst("UserId").Value.ToString();
            return result;
        }
    }
}