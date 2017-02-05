using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using OnTheRoad.Domain.Repositories;
using AutoMapper;

namespace OnTheRoad.Data.Repositories
{
    public abstract class BaseRepository<EntityType, DomainType> : IRepository<DomainType>
        where EntityType : class
        where DomainType : class
    {
        public BaseRepository(OnTheRoadDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<EntityType>();
        }

        protected OnTheRoadDbContext Context { get; set; }

        protected DbSet<EntityType> DbSet { get; set; }

        public void Add(DomainType entity)
        {
            Mapper.Initialize(config => config.CreateMap<DomainType, EntityType>());
            var mapped = Mapper.Map<DomainType, EntityType>(entity);

            var entry = this.Context.Entry(mapped);
            entry.State = EntityState.Added;
        }

        public void Delete(DomainType entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainType> GetAll()
        {
            var result = new List<DomainType>();
            Mapper.Initialize(config => config.CreateMap<EntityType, DomainType>());
            foreach (var entity in this.DbSet.ToList())
            {
                result.Add(Mapper.Map<EntityType, DomainType>(entity));
            }

            return result;
        }

        public DomainType GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Update(DomainType entity)
        {
            throw new NotImplementedException();
        }
    }
}
