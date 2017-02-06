using OnTheRoad.Domain.Contracts;
using OnTheRoad.Domain.Models;
using OnTheRoad.Domain.Repositories;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Logic.Models;
using System;
using System.Collections.Generic;

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

        public void DeleteCategoryByName(string name)
        {
            var category = this.GetCategoryByName(name);
            this.categoryRepository.Delete(category);
            this.uniOfWork.Commit();
        }

        public IEnumerable<ICategory> GetAllCategories()
        {
            var categories = this.categoryRepository.GetAll();

            return categories;
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
