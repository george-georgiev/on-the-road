using System;

namespace OnTheRoad.Domain.Models
{
    public interface IReview : IIdentifiable
    {
        IRating Rating { get; set; }

        string ReviewContent { get; set; }

        IUser FromUser { get; set; }

        IUser ToUser { get; set; }

        DateTime PostingDate { get; set; }
    }
}
