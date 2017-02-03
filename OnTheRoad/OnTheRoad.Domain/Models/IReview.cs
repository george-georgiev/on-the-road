using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Domain.Models
{
    public interface IReview
    {
        Rating Rating { get; }

        string Comment { get; }

        IUser FromUser { get; }

        IUser ToUser { get; }
    }
}
