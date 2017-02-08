using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using OnTheRoad.Domain.Repositories;
using AutoMapper;
using OnTheRoad.Domain.Models;
using OnTheRoad.Data.Models;
using System;

namespace OnTheRoad.Data.Repositories
{
    public abstract class BaseRepository<EntityType, DomainType> : IRepository<DomainType>
        where EntityType : BaseEntity
        where DomainType : IIdentifiable
    {
        public BaseRepository(OnTheRoadIdentityDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<EntityType>();
        }

        protected OnTheRoadIdentityDbContext Context { get; set; }

        protected DbSet<EntityType> DbSet { get; set; }

        public void Add(DomainType model)
        {
            this.SetEntityState(model, EntityState.Added);
        }

        public void Delete(DomainType model)
        {
            this.SetEntityState(model, EntityState.Deleted);
        }

        public IEnumerable<DomainType> GetAll()
        {
            Mapper.Initialize(config => config.CreateMap<EntityType, DomainType>());

            var mapped = new List<DomainType>();
            foreach (var entity in this.DbSet.ToList())
            {
                mapped.Add(Mapper.Map<EntityType, DomainType>(entity));
            }

            return mapped;
        }

        public DomainType GetById(object id)
        {
            Mapper.Initialize(config => config.CreateMap<EntityType, DomainType>());

            var found = this.DbSet.Find(id);
            var mapped = Mapper.Map<EntityType, DomainType>(found);

            return mapped;
        }

        public void Update(DomainType model)
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
                Mapper.Initialize(config => config.CreateMap<DomainType, EntityType>());
                entity = Mapper.Map<DomainType, EntityType>(model);
            }

            var entry = this.Context.Entry(entity);
            entry.State = entityState;
        }
    }
}
