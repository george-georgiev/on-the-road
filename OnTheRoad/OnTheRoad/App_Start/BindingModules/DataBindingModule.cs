using Ninject.Modules;
using Ninject.Web.Common;
using OnTheRoad.Data;
using OnTheRoad.Data.Repositories;
using OnTheRoad.Domain.Contracts;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Logic.Services;

namespace OnTheRoad.App_Start.BindingModules
{
    public class DataBindingModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<OnTheRoadIdentityDbContext>()
                .ToSelf()
                .InRequestScope();

            this.Bind<IUnitOfWork>()
                .To<UnitOfWork>();

            this.Bind<ICategoryRepository>()
                .To<CategoryRepository>();

            this.Bind<IUserService>()
                .To<UserService>();

            this.Bind<IUserRepository>()
                .To<UserRepository>();
        }
    }
}