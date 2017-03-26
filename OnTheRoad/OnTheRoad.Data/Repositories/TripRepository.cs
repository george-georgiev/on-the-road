using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using OnTheRoad.Data.Models;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using System.Data.Entity;
using System;
using OnTheRoad.Data.Contracts;

namespace OnTheRoad.Data.Repositories
{
    public class TripRepository : BaseRepository<Trip, ITrip>, ITripRepository, IGetRepository<ITrip>, IModifyRepository<ITrip>
    {
        public TripRepository(IOnTheRoadDbContext context) : base(context)
        {
        }

        public IEnumerable<ITrip> GetTripsByCategoryName(string categoryName, int skip, int take)
        {
            if (categoryName == null)
            {
                throw new ArgumentNullException("categoryName can not be null!");
            }

            var trips = this.GetByCategoryName(categoryName)
                .OrderByDescending(t => t.CreateDate)
                .Skip(skip)
                .Take(take);

            var mapped = this.MapTrips(trips);

            return mapped;
        }

        public int GetTripsCountByCategoryName(string categoryName)
        {
            if (categoryName == null)
            {
                throw new ArgumentNullException("categoryName can not be null!");
            }

            var count = this.GetByCategoryName(categoryName)
                .Count();

            return count;
        }

        public IEnumerable<ITrip> GetTripsByCategoryNameOrderedByDate(string categoryName, int count, bool isAscending)
        {
            var trips = this.GetByCategoryName(categoryName);
            if (isAscending)
            {
                trips = trips.OrderBy(t => t.CreateDate);
            }
            else
            {
                trips = trips.OrderByDescending(t => t.CreateDate);
            }

            var mapped = this.MapTrips(trips.Take(count));

            return mapped;
        }

        public IEnumerable<ITrip> GetTripsBySearchPattern(string pattern, int skip, int take)
        {
            var trips = this.GetBySearchPattern(pattern)
                .OrderByDescending(x => x.CreateDate)
                .Skip(skip)
                .Take(take);

            var mapped = this.MapTrips(trips);

            return mapped;
        }

        public int GetTripsCountBySearchPattern(string pattern)
        {
            var count = this.GetBySearchPattern(pattern).Count();

            return count;
        }

        public IEnumerable<ITrip> GetTrips(int skip, int take)
        {
            var trips = this.GetTrips()
                .OrderByDescending(t => t.CreateDate)
                .Skip(skip)
                .Take(take);

            var mapped = this.MapTrips(trips);

            return mapped;
        }

        public int GetTripsCount()
        {
            var count = this.GetTrips().Count();

            return count;
        }

        public override void Add(ITrip model)
        {
            base.Add(model);

            var entity = this.DbSet.Local.Where(e => e.Id == model.Id).FirstOrDefault();

            var organiser = model.Organiser;
            this.AddOrganiserToTrip(entity, organiser);

            var categories = model.Categories;
            this.AddCategoriesToTrip(entity, categories);

            var tags = model.Tags;
            this.AddTagsToTrip(entity, tags);
        }

        protected override ITrip MapEntityToDomain(Trip entity)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Trip, ITrip>()
                    .ForMember(x => x.Tags, opt => opt.Ignore());

                config.CreateMap<Category, ICategory>();

                config.CreateMap<Subscription, ISubscription>()
                    .ForMember(x => x.Trip, opt => opt.Ignore());

                config.CreateMap<User, IUser>()
                    .ForMember(x => x.City, opt => opt.Ignore())
                    .ForMember(x => x.FavouriteUsers, opt => opt.Ignore())
                    .ForMember(x => x.GivenReviews, opt => opt.Ignore())
                    .ForMember(x => x.ReceivedReviews, opt => opt.Ignore())
                    .ForMember(x => x.Subscriptions, opt => opt.Ignore())
                    .ForMember(x => x.Conversations, opt => opt.Ignore());
            });

            var domain = Mapper.Map<Trip, ITrip>(entity);

            return domain;
        }

        protected override void InitializeDomainToEnityMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<ITrip, Trip>()
                    .ForMember(x => x.Tags, opt => opt.Ignore())
                    .ForMember(x => x.Categories, opt => opt.Ignore())
                    .ForMember(x => x.Subscriptions, opt => opt.Ignore())
                    .ForMember(x => x.Organiser, opt => opt.Ignore());
            });
        }

        private IQueryable<Trip> GetByCategoryName(string categoryName)
        {
            var trips = this.Context.Trips
                            .Where(
                                t => t.Categories
                                    .Where(c => c.Name == categoryName)
                                    .Any()
                            )
                            .Include(x => x.Categories)
                            .Include(x => x.Organiser)
                            .Include(x => x.Subscriptions)
                            .Include(
                                x => x.Subscriptions
                                    .Select(s => s.User)
                            );

            return trips;
        }

        private IQueryable<Trip> GetBySearchPattern(string pattern)
        {
            var patternToLower = pattern.ToLower();
            var trips = this.Context.Trips
                            .Where(
                                x => x.Tags
                                    .Where(t => t.Name.ToLower().Contains(patternToLower))
                                    .Any()
                                )
                                .Include(x => x.Categories)
                                .Include(x => x.Organiser)
                                .Include(x => x.Subscriptions)
                                .Include(
                                    x => x.Subscriptions
                                        .Select(s => s.User)
                                );

            return trips;
        }

        private IEnumerable<ITrip> MapTrips(IQueryable<Trip> trips)
        {
            var mapped = new List<ITrip>();
            foreach (var trip in trips)
            {
                mapped.Add(this.MapEntityToDomain(trip));
            }

            return mapped;
        }

        private void AddOrganiserToTrip(Trip trip, IUser user)
        {
            var userEntity = this.Context.Users.Where(u => u.UserName == user.Username).FirstOrDefault();
            if (userEntity == null)
            {
                throw new ArgumentException("user not found!");
            }

            trip.Organiser = userEntity;
        }

        private void AddCategoriesToTrip(Trip trip, IEnumerable<ICategory> categories)
        {
            foreach (var item in categories)
            {
                var category = this.Context.Categories.Find(item.Id);
                trip.Categories.Add(category);
            }
        }

        private void AddTagsToTrip(Trip trip, IEnumerable<ITag> tags)
        {
            foreach (var item in tags)
            {
                var tag = this.Context.Tags.Find(item.Id);
                trip.Tags.Add(tag);
            }
        }

        private IQueryable<Trip> GetTrips()
        {
            var trips = this.Context.Trips
                            .Include(x => x.Categories)
                            .Include(x => x.Organiser)
                            .Include(x => x.Subscriptions)
                            .Include(
                                x => x.Subscriptions
                                    .Select(s => s.User)
                            );

            return trips;
        }
    }
}
