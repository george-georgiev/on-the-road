using AutoMapper;
using OnTheRoad.Data.Models;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnTheRoad.Data.Repositories
{
    public class TagRepository : BaseRepository<Tag, ITag>, ITagRepository
    {
        public TagRepository(OnTheRoadIdentityDbContext context) : base(context)
        {
        }

        public ITag GetTagByName(string name)
        {
            Mapper.Initialize(config => config.CreateMap<Tag, ITag>());

            var entity = this.DbSet.ToList().Where(t => t.Name == name).FirstOrDefault();
            var mapped = Mapper.Map<Tag, ITag>(entity);

            return mapped;
        }

        public IEnumerable<ITag> GetTagsByPrefix(string prefix)
        {
            throw new NotImplementedException();
        }
    }
}
