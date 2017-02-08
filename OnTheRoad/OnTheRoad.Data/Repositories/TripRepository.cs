using AutoMapper;
using OnTheRoad.Data.Models;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace OnTheRoad.Data.Repositories
{
    public class TripRepository : BaseRepository<Trip, ITrip>, ITripRepository
    {
        public TripRepository(OnTheRoadIdentityDbContext context) : base(context)
        {
        }

        public IEnumerable<ITrip> GetTripsByCategoryName(string categoryName)
        {
            var trips = this.Context.Trips
                .Where(
                    t => t.Categories
                        .Where(c => c.Name == categoryName)
                        .Any()
                );

            var mapped = MapTrips(trips);

            return mapped;
        }

        public IEnumerable<ITrip> GetTripsOrderedByDateCreated(int count, bool isAscending)
        {
            IQueryable<Trip> trips;
            if (isAscending)
            {
                trips = this.Context.Trips.OrderBy(t => t.StartDate).Take(count);
            }
            else
            {
                trips = this.Context.Trips.OrderByDescending(t => t.StartDate).Take(count);
            }

            var mapped = MapTrips(trips);

            return mapped;
        }

        private static IEnumerable<ITrip> MapTrips(IQueryable<Trip> trips)
        {
            var mapped = new List<ITrip>();
            Mapper.Initialize(config =>
            {
                config.CreateMap<Trip, ITrip>();
                config.CreateMap<Tag, ITag>();
                config.CreateMap<Category, ICategory>();
                config.CreateMap<TripImage, IImage>();
            });

            foreach (var category in trips)
            {
                mapped.Add(Mapper.Map<Trip, ITrip>(category));
            }

            return mapped;
        }
    }
}
