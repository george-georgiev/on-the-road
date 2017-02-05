using OnTheRoad.Domain.Contracts;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Logic.Models;
using System;

namespace OnTheRoad.Logic.Services
{
    // TODO implement ICategoryService inteface
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository<ICategory> categoryRepository;
        private readonly IUnitOfWork uniOfWork;

        public CategoryService(ICategoryRepository<ICategory> categoryRepository, IUnitOfWork uniOfWork)
        {
            if (categoryRepository == null)
            {
                throw new ArgumentNullException("categoryRepository can not be null!");
            }

            if (uniOfWork == null)
            {
                throw new ArgumentNullException("uniOfWork can not be null!");
            }

            this.categoryRepository = categoryRepository;
            this.uniOfWork = uniOfWork;
        }

        public void AddCategory(string name)
        {
            var category = new Category(name);
            this.categoryRepository.Add(category);
            this.uniOfWork.Commit();
        }

        public ICategory GetCategoryByName(string name)
        {
            return this.categoryRepository.GetCategoryByName(name);
        }
    }
}
