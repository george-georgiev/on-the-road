using OnTheRoad.Infrastructure.Enums;
using OnTheRoad.MVC.Contracts;
using System.Web;
using System;

namespace OnTheRoad.MVC.Utils
{
    public class ControllerUtil : IControllerUtil
    {
        public string LoggedUserName
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.Request.IsAuthenticated;
            }
        }

        public void SetResponseStatusCode(ResponseStatus status)
        {
            HttpContext.Current.Response.StatusCode = (int)status;
        }
    }
}