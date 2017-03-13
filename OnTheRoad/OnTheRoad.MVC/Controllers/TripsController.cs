using System.Web.Mvc;

namespace OnTheRoad.MVC.Controllers
{
    public class TripsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddTrip()
        {
            return this.View();
        }
    }
}