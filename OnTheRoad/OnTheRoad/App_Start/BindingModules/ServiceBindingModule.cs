using Ninject.Modules;
using Ninject.Extensions.Conventions;
using OnTheRoad.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using OnTheRoad.Logic.Services;
using OnTheRoad.Logic.Factories;
using Ninject.Extensions.Factory;
using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Models;

namespace OnTheRoad.App_Start.BindingModules
{
    public class ServiceBindingModule : NinjectModule
    {
        private const string TagName = "Tag";
        private const string ImageName = "Image";

        /// <summary>
        /// Binds all services.
        /// </summary>
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

            this.Bind<ITripAddService, ITripGetService>()
                .To<TripService>();

            this.Bind<ITag>()
                .To<Tag>()
                .Named(TagName);

            this.Bind<ITagFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ITagGetService>()
                .To<TagService>();

            this.Bind<ICategoryGetService, ICategoryModifyService>()
                .To<CategoryService>();

            this.Bind<IGetUserService>()
                .To<UserService>();
        }

        private IEnumerable<Type> GetTypesToExclude()
        {
            return new List<Type>() { typeof(Tag) };
        }
    }
}