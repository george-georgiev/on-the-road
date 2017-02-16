using System.Data.Entity;
using OnTheRoad.Data.Models;
using OnTheRoad.Data.Repositories;

namespace OnTheRoad.Data.Tests.Fakes
{
    public class UserRepositoryFake : UserRepository
    {
        public UserRepositoryFake(OnTheRoadIdentityDbContext context)
            : base(context)
        {
        }

        protected override void SetEntityState(User entity, EntityState entityState)
        {
            // Do nothing.
        }
    }
}
