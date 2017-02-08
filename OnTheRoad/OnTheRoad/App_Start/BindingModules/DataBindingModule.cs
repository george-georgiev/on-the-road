using Ninject.Modules;
using Ninject.Web.Common;
using OnTheRoad.Data;
using OnTheRoad.Data.Repositories;
using OnTheRoad.Domain.Contracts;
using OnTheRoad.Domain.Repositories;

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

            this.Bind<ITripRepository>()
                .To<TripRepository>();

            this.Bind<IUserRepository>()
                .To<UserRepository>();
        }
    }
}