using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Common;
using OnTheRoad.MVC.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ITripGetService tripGetService;

        public CategoriesController(ICategoryService categoryService, ITripGetService tripGetService)
        {
            if (categoryService == null)
            {
                throw new ArgumentNullException("categoryService cannot be null!");
            }

            if (tripGetService == null)
            {
                throw new ArgumentNullException("tripGetService cannot be null!");
            }

            this.categoryService = categoryService;
            this.tripGetService = tripGetService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var categories = this.categoryService.GetAllCategories();
            var categoriesModel = new List<CategoryViewModel>();
            foreach (var category in categories)
            {
                var mappedCategory = MapperProvider.Mapper.Map<CategoryViewModel>(category);
                categoriesModel.Add(mappedCategory);
            }

            return this.View(categoriesModel);
        }

        [HttpGet]
        public ActionResult ByName(string categoryName)
        {
            return this.View();
        }
    }
}