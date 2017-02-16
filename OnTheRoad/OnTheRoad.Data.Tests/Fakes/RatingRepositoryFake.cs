using System.Data.Entity;
using OnTheRoad.Data.Models;
using OnTheRoad.Data.Repositories;

namespace OnTheRoad.Data.Tests.Fakes
{
    class RatingRepositoryFake : RatingRepository
    {
        public RatingRepositoryFake(OnTheRoadIdentityDbContext context)
            : base(context)
        {
        }
        
    }
}
