using AutoMapper;
using OnTheRoad.Data.Models;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
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
            var entity = this.DbSet.ToList().Where(t => t.Name == name).FirstOrDefault();
            var mapped = this.MapEntityToDomain(entity);

            return mapped;
        }

        public IEnumerable<ITag> GetTagsByNamePrefix(string prefix, int take)
        {
            var prefixToLower = prefix.ToLower();
            var tags = this.DbSet.ToList()
                .Where(
                    t => t.Name.ToLower().IndexOf(prefixToLower) == 0
                )
                .OrderBy(t => t.Name)
                .Take(take);

            var mapped = new List<ITag>();
            foreach (var tag in tags)
            {
                mapped.Add(this.MapEntityToDomain(tag));
            }

            return mapped;
        }
    }
}
