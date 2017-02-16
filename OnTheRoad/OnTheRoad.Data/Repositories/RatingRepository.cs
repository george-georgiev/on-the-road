using System.Linq;
using AutoMapper;
using OnTheRoad.Data.Models;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using System;
using System.Data.Entity;

namespace OnTheRoad.Data.Repositories
{
    public class RatingRepository : BaseRepository<Rating, IRating>, IRatingRepository
    {
        public RatingRepository(OnTheRoadIdentityDbContext context)
            : base(context)
        {
        }

        public IRating GetByValue(string value)
        {
            Rating entity;

            try
            {
                entity = this.DbSet.Where(x => x.Value == value).Single();
            }
            catch (Exception)
            {
                throw new ArgumentException("The provided value doesn't exist in the rating system!");
            }

            Mapper.Initialize(config => config.CreateMap<Rating, IRating>());
            var mapped = Mapper.Map<Rating, IRating>(entity);
            return mapped;
        }
    }
}
