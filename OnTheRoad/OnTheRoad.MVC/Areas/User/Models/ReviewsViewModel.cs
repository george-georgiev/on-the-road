using System.Collections.Generic;

namespace OnTheRoad.MVC.Areas.User.Models
{
    public class ReviewsViewModel
    {
        public ReviewViewModel NewReview { get; set; }

        public IEnumerable<ReviewViewModel> Reviews { get; set; }

        public string Username { get; set; }

        public bool IsOwner { get; set; }

        public int Page { get; set; }

        public int Total { get; set; }

        public int Take { get; set; }
    }
}