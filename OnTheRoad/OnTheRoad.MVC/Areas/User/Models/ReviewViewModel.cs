using OnTheRoad.Domain.Models;
using OnTheRoad.Infrastructure.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnTheRoad.MVC.Areas.User.Models
{
    public class ReviewViewModel : IMapFrom<IReview>
    {
        [UIHint("Textarea")]
        public string ReviewContent { get; set; }

        public string RatingValue { get; set; }

        public byte[] FromUserImage { get; set; }

        public string FromUserUsername { get; set; }

        public DateTime PostingDate { get; set; }
    }
}