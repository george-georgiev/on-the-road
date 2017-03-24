using OnTheRoad.Domain.Enumerations;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.MVC.Areas.User.Models;
using OnTheRoad.MVC.Common;
using OnTheRoad.MVC.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Areas.User.Controllers
{
    public class ReviewsController : Controller
    {
        private const int Take = 3;

        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            if (reviewService == null)
            {
                throw new ArgumentNullException("reviewService cannot be null.");
            }

            this.reviewService = reviewService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index(string username, int page = 1)
        {
            page = page > 0 ? page : 1;
            var skip = (page - 1) * Take;
            var reviews = this.reviewService.GetUserReviews(username, skip, Take);

            var mappedReviews = MapperProvider.Mapper.Map<IEnumerable<ReviewViewModel>>(reviews);

            var model = new ReviewsViewModel();
            model.Reviews = mappedReviews;

            var total = this.reviewService.GetUserReviewsTotal(username);

            var loggedUsername = ControllerUtilProvider.ControllerUtil.LoggedUserName;
            var isOwner = loggedUsername == username;
            model.IsOwner = isOwner;

            model.Take = Take;
            model.Page = page;
            model.Username = username;
            model.Total = total;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(ReviewsViewModel review)
        {
            var content = review.NewReview.ReviewContent;
            var fromUsername = ControllerUtilProvider.ControllerUtil.LoggedUserName;
            var toUsername = review.Username;
            var ratingValue = review.NewReview.RatingValue;
            var rating = (RatingEnum)Enum.Parse(typeof(RatingEnum), ratingValue);
            this.reviewService.AddUserReview(content, fromUsername, toUsername, rating, DateTime.Now);

            return RedirectToAction("Index", "Reviews", new { username = toUsername });
        }
    }
}