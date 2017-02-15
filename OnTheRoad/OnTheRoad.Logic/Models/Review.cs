using System;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Models
{
    public class Review : IReview
    {
        public Review(string reviewContent, IUser fromUser, IUser toUser, IRating rating, DateTime postingDate)
        {
            this.ReviewContent = reviewContent;
            this.FromUser = fromUser;
            this.ToUser = toUser;
            this.Rating = rating;
            this.PostingDate = postingDate;
        }

        public int Id { get; set; }

        public string ReviewContent { get; set; }

        public IUser FromUser { get; set; }

        public IUser ToUser { get; set; }

        public IRating Rating { get; set; }

        public DateTime PostingDate { get; set; }
    }
}
