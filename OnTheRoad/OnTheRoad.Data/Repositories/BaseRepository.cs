using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using OnTheRoad.Data.Models;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;

namespace OnTheRoad.Data.Repositories
{
    public abstract class BaseRepository<EntityType, DomainType> : IGetRepository<DomainType>, IModifyRepository<DomainType>
        where EntityType : BaseEntity
        where DomainType : IIdentifiable
    {
        public BaseRepository(OnTheRoadIdentityDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context cannot be null!");
            }

            this.Context = context;
            this.DbSet = this.Context.Set<EntityType>();
        }

        protected OnTheRoadIdentityDbContext Context { get; }

        protected DbSet<EntityType> DbSet { get; }

        public virtual void Add(DomainType model)
        {
            this.SetEntityState(model, EntityState.Added);
        }

        public virtual void Delete(DomainType model)
        {
            this.SetEntityState(model, EntityState.Deleted);
        }

        public virtual IEnumerable<DomainType> GetAll()
        {
            var mapped = new List<DomainType>();
            foreach (var entity in this.DbSet.ToList())
            {
                mapped.Add(this.MapEntityToDomain(entity));
            }

            return mapped;
        }

        public virtual DomainType GetById(object id)
        {
            var found = this.DbSet.Find(id);
            var mapped = this.MapEntityToDomain(found);

            return mapped;
        }

        public virtual void Update(DomainType model)
        {
            this.SetEntityState(model, EntityState.Modified);
        }

        private void SetEntityState(DomainType model, EntityState entityState)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model can not be null!");
            }

            var entity = this.DbSet.Local.Where(e => e.Id == model.Id).FirstOrDefault();
            if (entity == null)
            {
                entity = this.MapDomainToEnity(model);
            }

            this.SetEntityStateHelper(entity, entityState);
        }

        protected virtual void SetEntityStateHelper(EntityType entity, EntityState entityState)
        {
            var entry = this.Context.Entry(entity);
            entry.State = entityState;
        }

        protected virtual EntityType MapDomainToEnity(DomainType domain)
        {
            Mapper.Initialize(config => config.CreateMap<DomainType, EntityType>());
            var entity = Mapper.Map<DomainType, EntityType>(domain);

            return entity;
        }

        protected virtual DomainType MapEntityToDomain(EntityType entity)
        {
            Mapper.Initialize(config => config.CreateMap<EntityType, DomainType>());
            var domain = Mapper.Map<EntityType, DomainType>(entity);

            return domain;
        }
    }
}
