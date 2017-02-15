using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OnTheRoad.Data.Models;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;

namespace OnTheRoad.Data.Repositories
{
    public class ReviewRepository : BaseRepository<Review, IReview>, IReviewRepository, IGetRepository<IReview>, IModifyRepository<IReview>
    {
        public ReviewRepository(OnTheRoadIdentityDbContext context)
            : base(context)
        {
        }

        public IEnumerable<IReview> GetByToUser(IUser toUser)
        {
            // TODO: Check if it works?
            var entities = this.DbSet.Where(x => x.ToUserId == toUser.Id).ToList();
            Mapper.Initialize(config => config.CreateMap<Review, IReview>());

            List<IReview> reviews = new List<IReview>();
            foreach (var entity in entities)
            {
                var review = Mapper.Map<Review, IReview>(entity);
                reviews.Add(review);
            }

            return reviews;
        }

        protected override Review MapDomainToEnity(IReview domain)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<IRating, Rating>();
                config.CreateMap<IUser, User>();
                config.CreateMap<ISubscription, Subscription>();
                config.CreateMap<ICity, City>();

                config.CreateMap<IReview, Review>()
                .ForMember(x => x.Rating, opt => opt.Ignore())
                .ForMember(x => x.FromUser, opt => opt.Ignore())
                .ForMember(x => x.ToUser, opt => opt.Ignore());
            });

            var entity = Mapper.Map<IReview, Review>(domain);

            return entity;
        }

        //private void MapReviewToIReview()
        //{
        //    Mapper.Initialize(config => config.CreateMap<Review, IReview>());
        //}

        //private void MapIReviewToReview(IReview model)
        //{
        //    Mapper.Initialize(config => config.CreateMap<IReview, Review>());
        //}
    }
}
