using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Filters;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Controllers
{
    public class TagsController : Controller
    {
        private const int Count = 10;

        private readonly ITagService tagService;

        public TagsController(ITagService tagService)
        {
            if (tagService == null)
            {
                throw new ArgumentNullException("tagService can not be null!");
            }

            this.tagService = tagService;
        }

        [HttpGet]
        [Ajax]
        [Authorize]
        public ActionResult Search(string term)
        {
            var tags = this.tagService
                .GetTagsByNamePrefix(term, Count)
                .Select(t => t.Name)
                .ToArray();
            
            return Json(tags, JsonRequestBehavior.AllowGet);
        }
    }
}