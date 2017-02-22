using System;
using System.Collections.Generic;
using OnTheRoad.Domain.Contracts;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Logic.Contracts;

namespace OnTheRoad.Logic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork uniOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork uniOfWork)
        {
            if (categoryRepository == null)
            {
                throw new ArgumentNullException("categoryRepository cannot be null!");
            }

            if (uniOfWork == null)
            {
                throw new ArgumentNullException("uniOfWork cannot be null!");
            }

            this.categoryRepository = categoryRepository;
            this.uniOfWork = uniOfWork;
        }

        public IEnumerable<ICategory> GetAllCategories()
        {
            var categories = this.categoryRepository.GetAll();

            return categories;
        }

        public IEnumerable<ICategory> GetCategoriesByIdCollection(IEnumerable<int> idCollection)
        {
            var result = new List<ICategory>();

            foreach (var id in idCollection)
            {
                var category = this.GetCategoryById(id);
                result.Add(category);
            }

            return result;
        }

        public ICategory GetCategoryById(int id)
        {
            var category = this.categoryRepository.GetById(id);

            return category;
        }

        public ICategory GetCategoryByName(string name)
        {
            return this.categoryRepository.GetCategoryByName(name);
        }
    }
}
