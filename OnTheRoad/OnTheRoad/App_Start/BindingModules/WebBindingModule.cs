using Ninject.Modules;
using OnTheRoad.Common;
using OnTheRoad.Logic.Contracts;
using System.Web.Caching;

namespace OnTheRoad.App_Start.BindingModules
{
    public class WebBindingModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IImageResizer>()
                .To<CustomImageResizer>();

            //this.Bind<Cache>()
            //    .ToSelf()
            //    .InSingletonScope();
        }
    }
}