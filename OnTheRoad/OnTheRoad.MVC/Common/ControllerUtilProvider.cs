using OnTheRoad.MVC.Contracts;
using OnTheRoad.MVC.Utils;

namespace OnTheRoad.MVC.Common
{
    public static class ControllerUtilProvider
    {
        static ControllerUtilProvider()
        {
            ControllerUtilProvider.ControllerUtil = new ControllerUtil();
        }

        public static IControllerUtil ControllerUtil { get; set; }
    }
}