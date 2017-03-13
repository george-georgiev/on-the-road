using System.Web.Mvc;

namespace OnTheRoad.MVC.Areas.Administration.Controllers
{
    public class ManageTripsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}