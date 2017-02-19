using System;
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
    public abstract class BaseRepository<EntityType, DomainType> : IGetRepository<DomainType>, IModifyRepository<DomainType>
        where EntityType : BaseEntity
        where DomainType : IIdentifiable
    {
        public BaseRepository(IOnTheRoadDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context cannot be null!");
            }

            this.Context = context;
            this.DbSet = this.Context.Set<EntityType>();
        }

        protected IOnTheRoadDbContext Context { get; }

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

        protected virtual void SetEntityState(DomainType model, EntityState entityState)
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
            else
            {
                entity = this.MapDomainToEnity(model, entity);
            }

            this.Context.SetEntryState(entity, entityState);
        }

        private EntityType MapDomainToEnity(DomainType domain)
        {
            this.InitializeDomainToEnityMapper();
            var entity = Mapper.Map<DomainType, EntityType>(domain);

            return entity;
        }

        private EntityType MapDomainToEnity(DomainType domain, EntityType entity)
        {
            this.InitializeDomainToEnityMapper();
            var mapped = Mapper.Map<DomainType, EntityType>(domain, entity);

            return mapped;
        }

        protected virtual void InitializeDomainToEnityMapper()
        {
            Mapper.Initialize(config => config.CreateMap<DomainType, EntityType>());
        }

        protected virtual DomainType MapEntityToDomain(EntityType entity)
        {
            Mapper.Initialize(config => config.CreateMap<EntityType, DomainType>());
            var domain = Mapper.Map<EntityType, DomainType>(entity);

            return domain;
        }
    }
}
