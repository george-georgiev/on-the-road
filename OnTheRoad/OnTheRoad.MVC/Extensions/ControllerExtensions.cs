using OnTheRoad.Infrastructure.Common;
using OnTheRoad.Infrastructure.Enums;
using OnTheRoad.Infrastructure.Wrappers;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Extensions
{
    public static class ControllerExtensions
    {
        public static ToastMessage AddToastMessage(this Controller controller, string title, string message, ToastType toastType = ToastType.Info)
        {
            var toastr = controller.TempData[GlobalConstants.Toastr] as Toastr;
            toastr = toastr ?? new Toastr();

            var toastMessage = toastr.AddToastMessage(title, message, toastType);
            controller.TempData[GlobalConstants.Toastr] = toastr;
            return toastMessage;
        }
    }
}