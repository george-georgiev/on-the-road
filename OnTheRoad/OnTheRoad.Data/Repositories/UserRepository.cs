using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Data.Models;

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


        // TODO: Implement
        public IEnumerable<IUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public IUser GetByUserName(string username)
        {
            this.MapUserToIUser();
            var found = this.DbSet.Where(x => x.UserName == username).FirstOrDefault();
            if (found == null)
            {
                return null;
            }

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

        public void RemoveFavouriteUser(string userId, string userToRemoveUsername)
        {
            var entity = this.DbSet.Local.Where(e => e.Id == userId).FirstOrDefault();

            var userToRemove = entity.FavouriteUsers.FirstOrDefault(x => x.UserName == userToRemoveUsername);
            if (userToRemove != null)
            {
                entity.FavouriteUsers.Remove(userToRemove);
            }

            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void AddFavouriteUser(string userId, string userToAddUsername)
        {
            var entity = this.DbSet.Local.Where(e => e.Id == userId).FirstOrDefault();

            var userToAdd = this.Context.Users.FirstOrDefault(x => x.UserName == userToAddUsername);
            if (userToAdd != null)
            {
                entity.FavouriteUsers.Add(userToAdd);
            }

            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;
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

        public void UpdateImage(byte[] image, string username)
        {
            var user = this.DbSet.Where(x => x.UserName == username).Single();
            user.Image = image;

            var entry = this.Context.Entry(user);
            entry.State = EntityState.Modified;
        }

        private void MapUserToIUser()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<User, IUser>();
                config.CreateMap<City, ICity>();
                config.CreateMap<Subscription, ISubscription>();
                config.CreateMap<Review, IReview>();
            });
        }

        private void MapIUserToUser(IUser model)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<IUser, User>()
                .ForMember(x => x.City, opt => opt.Ignore())
                .ForMember(x => x.FavouriteUsers, opt => opt.Ignore());
                config.CreateMap<ISubscription, Subscription>();
                config.CreateMap<IReview, Review>();
            });
        }
    }
}
