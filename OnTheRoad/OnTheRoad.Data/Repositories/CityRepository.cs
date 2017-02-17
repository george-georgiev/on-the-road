using OnTheRoad.Data.Contracts;
using OnTheRoad.Data.Models;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;

namespace OnTheRoad.Data.Repositories
{
    public class CityRepository : BaseRepository<City, ICity>, ICityRepository, IGetRepository<ICity>
    {
        public CityRepository(IOnTheRoadDbContext context)
            : base(context)
        {
        }
    }
}
