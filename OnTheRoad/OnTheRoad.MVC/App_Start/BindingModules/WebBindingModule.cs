using Ninject.Modules;
using OnTheRoad.MVC.Common;
using OnTheRoad.Logic.Contracts;
using System.Web.Caching;

namespace OnTheRoad.MVC.App_Start.BindingModules
{
    public class WebBindingModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IImageResizer>()
                .To<CustomImageResizer>();
        }
    }
}