using Ninject.Modules;
using Ninject.Extensions.Conventions;
using OnTheRoad.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace OnTheRoad.App_Start.BindingModules
{
    public class ServiceBindingModule : NinjectModule
    {
        public override void Load()
        {
            var typesToExclude = this.GetTypesToExclude();
            Kernel.Bind(x =>
            {
                x.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetAssembly(typeof(ILoginService)).Location))
                    .SelectAllClasses()
                    .Excluding(typesToExclude)
                    .BindDefaultInterface();
            });
        }

        private IEnumerable<Type> GetTypesToExclude()
        {
            return new List<Type>();
        }
    }
}