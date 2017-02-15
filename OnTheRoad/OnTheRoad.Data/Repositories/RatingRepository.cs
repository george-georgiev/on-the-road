using System.Linq;
using AutoMapper;
using OnTheRoad.Data.Models;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;


namespace OnTheRoad.Data.Repositories
{
    public class RatingRepository : BaseRepository<Rating, IRating>, IRatingRepository
    {
        public RatingRepository(OnTheRoadIdentityDbContext context)
            : base(context)
        {
        }

        public IRating GetByValue(string value)
        {
            var entity = this.DbSet.Where(x => x.Value == value).Single();
            Mapper.Initialize(config => config.CreateMap<Rating, IRating>());
            var mapped = Mapper.Map<Rating, IRating>(entity);

            return mapped;
        }
    }
}
