using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Domain.Models
{
    public interface IReview : IIdentifiable
    {
        Rating Rating { get; set; }

        string Comment { get; set; }

        IUser FromUser { get; set; }

        IUser ToUser { get; set; }
    }
}
