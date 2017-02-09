using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Data.Models;
using AutoMapper;
using System.Data.Entity;
using System.Linq;

namespace OnTheRoad.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(OnTheRoadIdentityDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<User>();
        }

        protected OnTheRoadIdentityDbContext Context { get; set; }

        protected DbSet<User> DbSet { get; set; }


        // TODO: not impemented
        public void Delete(IUser entity)
        {
        }

        public IEnumerable<IUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public IUser GetByUserName(string userName)
        {
            this.MapUserToIUser();
            var found = this.DbSet.Where(x => x.UserName == userName).Single();
            var mapped = Mapper.Map<User, IUser>(found);

            return mapped;
        }

        public IUser GetById(object id)
        {
            this.MapUserToIUser();
            var found = this.DbSet.Find(id);
            var mapped = Mapper.Map<User, IUser>(found);

            return mapped;
        }

        public void Update(IUser entity)
        {
            this.SetEntityState(entity, EntityState.Modified);
        }

        private void SetEntityState(IUser model, EntityState entityState)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model can not be null!");
            }

            this.MapIUserToUser(model);
            var entity = this.DbSet.Local.Where(e => e.Id == model.Id.ToString()).FirstOrDefault();
            if (entity == null)
            {
                entity = Mapper.Map<IUser, User>(model);
            }
            else
            {
                entity = Mapper.Map<IUser, User>(model, entity);
            }

            var entry = this.Context.Entry(entity);
            entry.State = entityState;
        }

        private void MapUserToIUser()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<User, IUser>();
                config.CreateMap<City, ICity>();
                config.CreateMap<Subscription, ISubscribtion>();
                config.CreateMap<Review, IReview>();
                config.CreateMap<Country, ICountry>();
                config.CreateMap<UserImage, IImage>();
            });
        }

        private void MapIUserToUser(IUser model)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<IUser, User>().ForMember(x => x.City, opt => opt.Ignore());
                config.CreateMap<ISubscribtion, Subscription>();
                config.CreateMap<IReview, Review>();
                config.CreateMap<ICountry, Country>();
                config.CreateMap<IImage, UserImage>();
            });
        }
    }
}
