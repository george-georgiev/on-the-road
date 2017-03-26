﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using OnTheRoad.Data.Models;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Data.Contracts;

namespace OnTheRoad.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(IOnTheRoadDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context cannot be null!");
            }

            this.Context = context;
            this.DbSet = this.Context.Set<User>();
        }

        protected IOnTheRoadDbContext Context { get; set; }

        protected DbSet<User> DbSet { get; set; }

        public IEnumerable<IUser> GetAll()
        {
            var mapped = new List<IUser>();
            this.MapUserToIUser();
            foreach (var entity in this.DbSet.ToList())
            {
                mapped.Add(this.GetMappedDomainUser(entity));
            }

            return mapped;
        }

        public IUser GetByUserName(string username)
        {
            this.MapUserToIUser();
            var entity = this.DbSet.Where(x => x.UserName == username).FirstOrDefault();
            if (entity == null)
            {
                return null;
            }

            var mapped = this.GetMappedDomainUser(entity);
            return mapped;
        }

        public IUser GetById(object id)
        {
            this.MapUserToIUser();
            var entity = this.DbSet.Find(id);
            if (entity == null)
            {
                return null;
            }

            var mapped = this.GetMappedDomainUser(entity);
            return mapped;
        }

        public int GetAllUsersCount()
        {
            int usersCount = this.DbSet.Count();
            return usersCount;
        }

        public void Update(IUser model)
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

            if (model.FavouriteUsers != null)
            {
                var updatedFavUsers = new List<User>();
                foreach (var fu in model.FavouriteUsers)
                {
                    var user = this.Context.Users.Where(e => e.Id == fu.Id.ToString()).Single();
                    updatedFavUsers.Add(user);
                }

                entity.FavouriteUsers = updatedFavUsers;
            }

            if (model.GivenReviews != null)
            {
                var updatedReviews = new List<Review>();
                foreach (var rev in model.GivenReviews)
                {
                    var r = this.Context.Reviews.Where(e => e.Id == rev.Id).Single();
                    updatedReviews.Add(r);
                }

                entity.GivenReviews = updatedReviews;
            }

            if (model.ReceivedReviews != null)
            {
                var updatedReviews = new List<Review>();
                foreach (var rev in model.ReceivedReviews)
                {
                    var r = this.Context.Reviews.Where(e => e.Id == rev.Id).Single();
                    updatedReviews.Add(r);
                }

                entity.ReceivedReviews = updatedReviews;
            }

            if (model.Subscriptions != null)
            {
                var updatedSubscriptions = new List<Subscription>();
                foreach (var subs in model.Subscriptions)
                {
                    var s = this.Context.Subscriptions.Where(e => e.Id == subs.Id).Single();
                    updatedSubscriptions.Add(s);
                }

                entity.Subscriptions = updatedSubscriptions;
            }

            this.Context.SetEntryState(entity, EntityState.Modified);
        }

        private IUser GetMappedDomainUser(User entity)
        {
            var mapped = Mapper.Map<User, IUser>(entity);
            if (entity.FavouriteUsers != null)
            {
                var updatedFavUsers = new List<IUser>();
                foreach (var fu in entity.FavouriteUsers)
                {
                    var mappedFavUser = Mapper.Map<User, IUser>(fu);
                    updatedFavUsers.Add(mappedFavUser);
                }

                mapped.FavouriteUsers = updatedFavUsers;
            }

            this.MapReviewToIReview();
            if (entity.GivenReviews != null)
            {
                var updatedReviews = new List<IReview>();
                foreach (var gr in entity.GivenReviews)
                {
                    var mappedReview = Mapper.Map<Review, IReview>(gr);
                    updatedReviews.Add(mappedReview);
                }

                mapped.GivenReviews = updatedReviews;
            }

            if (entity.ReceivedReviews != null)
            {
                var updatedReviews = new List<IReview>();
                foreach (var gr in entity.ReceivedReviews)
                {
                    var mappedReview = Mapper.Map<Review, IReview>(gr);
                    updatedReviews.Add(mappedReview);
                }

                mapped.ReceivedReviews = updatedReviews;
            }

            return mapped;
        }

        private void MapUserToIUser()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<City, ICity>();

                config.CreateMap<Subscription, ISubscription>()
                    .ForMember(x => x.User, opt => opt.Ignore());

                config.CreateMap<Trip, ITrip>()
                    .ForMember(x => x.Tags, opt => opt.Ignore())
                    .ForMember(x => x.Categories, opt => opt.Ignore())
                    .ForMember(x => x.Subscriptions, opt => opt.Ignore())
                    .ForMember(x => x.Organiser, opt => opt.Ignore());

                config.CreateMap<Rating, IRating>();

                config.CreateMap<User, IUser>()
                    .ForMember(x => x.FavouriteUsers, opt => opt.Ignore());

                config.CreateMap<Review, IReview>()
                    .ForMember(x => x.Rating, opt => opt.Ignore())
                    .ForMember(x => x.FromUser, opt => opt.Ignore())
                    .ForMember(x => x.ToUser, opt => opt.Ignore());
            });
        }

        private void MapIUserToUser(IUser model)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<IUser, User>()
                    .ForMember(x => x.City, opt => opt.Ignore())
                    .ForMember(x => x.FavouriteUsers, opt => opt.Ignore())
                    .ForMember(x => x.GivenReviews, opt => opt.Ignore())
                    .ForMember(x => x.ReceivedReviews, opt => opt.Ignore())
                    .ForMember(x => x.Conversations, opt => opt.Ignore());

                config.CreateMap<ISubscription, Subscription>()
                    .ForMember(x => x.User, opt => opt.Ignore());

                config.CreateMap<ITrip, Trip>()
                    .ForMember(x => x.Tags, opt => opt.Ignore())
                    .ForMember(x => x.Categories, opt => opt.Ignore())
                    .ForMember(x => x.Subscriptions, opt => opt.Ignore())
                    .ForMember(x => x.Organiser, opt => opt.Ignore());

                config.CreateMap<IReview, Review>();

                config.CreateMap<Review, IReview>();
            });
        }

        private void MapReviewToIReview()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Rating, IRating>();
                config.CreateMap<User, IUser>()
                    .ForMember(x => x.City, opt => opt.Ignore())
                    .ForMember(x => x.FavouriteUsers, opt => opt.Ignore())
                    .ForMember(x => x.GivenReviews, opt => opt.Ignore())
                    .ForMember(x => x.ReceivedReviews, opt => opt.Ignore())
                    .ForMember(x => x.Subscriptions, opt => opt.Ignore());

                config.CreateMap<Review, IReview>()
                    .ForMember(x => x.Rating, opt => opt.MapFrom(s => Mapper.Map<Rating, IRating>(s.Rating)))
                    .ForMember(x => x.FromUser, opt => opt.MapFrom(s => Mapper.Map<User, IUser>(s.FromUser)))
                    .ForMember(x => x.ToUser, opt => opt.MapFrom(s => Mapper.Map<User, IUser>(s.ToUser)));
            });
        }
    }
}
