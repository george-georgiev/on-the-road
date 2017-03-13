using System.Web.Mvc;

namespace OnTheRoad.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ByName(string categoryName)
        {
            return View();
        }
    }
}