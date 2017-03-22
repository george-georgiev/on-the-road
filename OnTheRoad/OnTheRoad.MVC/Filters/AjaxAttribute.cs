using System.Reflection;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Filters
{
    public class AjaxAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var isValid = controllerContext.HttpContext.Request.IsAjaxRequest();

            return isValid;
        }
    }
}