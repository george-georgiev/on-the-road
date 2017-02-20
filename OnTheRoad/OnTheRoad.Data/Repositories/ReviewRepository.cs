using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OnTheRoad.Data.Models;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Data.Contracts;

namespace OnTheRoad.Data.Repositories
{
    public class ReviewRepository : BaseRepository<Review, IReview>, IReviewRepository, IGetRepository<IReview>, IModifyRepository<IReview>
    {
        public ReviewRepository(IOnTheRoadDbContext context)
            : base(context)
        {
        }

        public IEnumerable<IReview> GetByToUser(string toUser)
        {
            if (!this.Context.Users.Any(u => u.UserName == toUser))
            {
                return null;
            }

            var entities = this.DbSet.Where(x => x.ToUser.UserName == toUser && x.IsDeleted == false).ToList();
            Mapper.Initialize(config => config.CreateMap<Review, IReview>());

            List<IReview> reviews = new List<IReview>();
            foreach (var entity in entities)
            {
                var review = this.MapEntityToDomain(entity);

                reviews.Add(review);
            }

            return reviews;
        }

        protected override void InitializeDomainToEnityMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<IRating, Rating>();
                config.CreateMap<ICity, City>();
                config.CreateMap<IUser, User>()
                    .ForMember(x => x.Subscriptions, opt => opt.Ignore());


                config.CreateMap<IReview, Review>()
                    .ForMember(x => x.Rating, opt => opt.Ignore())
                    .ForMember(x => x.FromUser, opt => opt.Ignore())
                    .ForMember(x => x.ToUser, opt => opt.Ignore());
            });
        }

        protected override IReview MapEntityToDomain(Review entity)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Rating, IRating>();
                config.CreateMap<User, IUser>()
                    .ForMember(x => x.City, opt => opt.Ignore())
                    .ForMember(x => x.FavouriteUsers, opt => opt.Ignore())
                    .ForMember(x => x.Subscriptions, opt => opt.Ignore())
                    .ForMember(x => x.GivenReviews, opt => opt.Ignore())
                    .ForMember(x => x.ReceivedReviews, opt => opt.Ignore());

                config.CreateMap<Review, IReview>()
                    .ForMember(x => x.Rating, opt => opt.MapFrom(s => Mapper.Map<Rating, IRating>(s.Rating)))
                    .ForMember(x => x.FromUser, opt => opt.MapFrom(s => Mapper.Map<User, IUser>(s.FromUser)))
                    .ForMember(x => x.ToUser, opt => opt.MapFrom(s => Mapper.Map<User, IUser>(s.ToUser)));
            });

            var domain = Mapper.Map<Review, IReview>(entity);
            return domain;
        }
    }
}
