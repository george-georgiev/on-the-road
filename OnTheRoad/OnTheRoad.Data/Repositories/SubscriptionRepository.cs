using System.Data.Entity;
using System.Linq;
using AutoMapper;
using OnTheRoad.Data.Contracts;
using OnTheRoad.Data.Models;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;

namespace OnTheRoad.Data.Repositories
{
    public class SubscriptionRepository : BaseRepository<Subscription, ISubscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(IOnTheRoadDbContext context) : base(context)
        {
        }

        public ISubscription GetSubscription(string username, int tripId)
        {
            var subscription = this.Context.Subscriptions
                .Where(s => s.User.UserName == username && s.TripId == tripId)
                .FirstOrDefault();

            if (subscription == null)
            {
                return null;
            }

            var model = this.MapEntityToDomain(subscription);

            return model;
        }

        protected override void InitializeDomainToEnityMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<ISubscription, Subscription>()
                    .ForMember(x => x.Trip, opt => opt.Ignore())
                    .ForMember(x => x.User, opt => opt.Ignore());
            });
        }

        protected override ISubscription MapEntityToDomain(Subscription entity)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Subscription, ISubscription>();

                config.CreateMap<User, IUser>()
                    .ForMember(x => x.City, opt => opt.Ignore())
                    .ForMember(x => x.FavouriteUsers, opt => opt.Ignore())
                    .ForMember(x => x.GivenReviews, opt => opt.Ignore())
                    .ForMember(x => x.ReceivedReviews, opt => opt.Ignore())
                    .ForMember(x => x.Subscriptions, opt => opt.Ignore());

                config.CreateMap<Trip, ITrip>()
                    .ForMember(x => x.Tags, opt => opt.Ignore())
                    .ForMember(x => x.Categories, opt => opt.Ignore())
                    .ForMember(x => x.Subscriptions, opt => opt.Ignore())
                    .ForMember(x => x.Organiser, opt => opt.Ignore());
            });

            var domain = Mapper.Map<Subscription, ISubscription>(entity);

            return domain;
        }

        protected override void SetEntityState(ISubscription model, EntityState entityState)
        {
            base.SetEntityState(model, entityState);

            var entity = this.DbSet.Local.Where(s => s.Id == model.Id).FirstOrDefault();
            this.AddUserToSubscriptionEntity(model, entity);
            this.AddTripToSubscriptionEntity(model, entity);
        }

        private void AddTripToSubscriptionEntity(ISubscription model, Subscription entity)
        {
            var tripId = model.Trip.Id;
            var trip = this.Context.Trips.Where(t => t.Id == tripId).FirstOrDefault();
            entity.Trip = trip;
        }

        private void AddUserToSubscriptionEntity(ISubscription model, Subscription entity)
        {
            var username = model.User.Username;
            var user = this.Context.Users.Where(u => u.UserName == username).FirstOrDefault();
            entity.User = user;
        }
    }
}
