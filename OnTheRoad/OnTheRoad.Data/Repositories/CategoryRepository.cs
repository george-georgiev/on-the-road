using OnTheRoad.Data.Models;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using System.Linq;
using AutoMapper;

namespace OnTheRoad.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category, ICategory>, ICategoryRepository<ICategory>, IRepository<ICategory>
    {
        public CategoryRepository(OnTheRoadDbContext context) : base(context)
        {
        }

        public ICategory GetCategoryByName(string name)
        {
            Mapper.Initialize(config => config.CreateMap<Category, ICategory>());
            var entity = this.Context.Categories.Where(c => c.Name == name).Single();
            var result = Mapper.Map<Category, ICategory>(entity);

            return result;
        }
    }
}
