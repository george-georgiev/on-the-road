using OnTheRoad.Data.Models;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using System.Linq;
using AutoMapper;
using OnTheRoad.Data.Contracts;

namespace OnTheRoad.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category, ICategory>, ICategoryRepository, IGetRepository<ICategory>
    {
        public CategoryRepository(IOnTheRoadDbContext context) : base(context)
        {
        }

        public ICategory GetCategoryByName(string name)
        {
            Mapper.Initialize(config => config.CreateMap<Category, ICategory>());

            var entity = this.Context.Categories.Where(c => c.Name == name).FirstOrDefault();
            var mapped = Mapper.Map<Category, ICategory>(entity);

            return mapped;
        }
    }
}
