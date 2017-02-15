using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Mvp.Models
{
    public class ReviewsModel
    {
        public IEnumerable<IReview> Reviews { get; set; }
    }
}
