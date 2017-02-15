using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Models
{
    public class Review : IReview
    {
        // TODO: Validate
        public Review(string reviewContent, IUser fromUser, IUser toUser, IRating rating)
        {
            this.ReviewContent = reviewContent;
            this.FromUser = fromUser;
            this.ToUser = toUser;
            this.Rating = rating;
        }

        public int Id { get; set; }

        public string ReviewContent { get; set; }

        public IUser FromUser { get; set; }

        public IUser ToUser { get; set; }

        public IRating Rating { get; set; }
    }
}
