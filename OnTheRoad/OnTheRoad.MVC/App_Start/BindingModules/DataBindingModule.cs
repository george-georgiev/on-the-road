using Ninject.Extensions.Conventions;
using Ninject.Modules;
using Ninject.Web.Common;
using OnTheRoad.Data;
using OnTheRoad.Data.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace OnTheRoad.MVC.App_Start.BindingModules
{
    public class DataBindingModule : NinjectModule
    {
        public override void Load()
        {
            var typesToExclude = this.GetTypesToExclude();
            Kernel.Bind(x =>
            {
                x.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetAssembly(typeof(OnTheRoadIdentityDbContext)).Location))
                    .SelectAllClasses()
                    .Excluding(typesToExclude)
                    .BindDefaultInterface();
            });

            this.Bind<IOnTheRoadDbContext>()
                .To<OnTheRoadIdentityDbContext>()
                .InRequestScope();
        }

        private IEnumerable<Type> GetTypesToExclude()
        {
            return new List<Type>();
        }
    }
}