using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Models;
using System;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Controllers
{
    public class NavigationPartialController : Controller
    {
        private readonly ICategoryService categoryService;

        public NavigationPartialController(ICategoryService categoryService)
        {
            if (categoryService == null)
            {
                throw new ArgumentNullException("categoryService can not be null!");
            }

            this.categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var categories = this.categoryService.GetAllCategories();
            var model = new NavigationPartialViewModel() { Categories = categories };

            return PartialView("_NavigationPartial", model);
        }
    }
}