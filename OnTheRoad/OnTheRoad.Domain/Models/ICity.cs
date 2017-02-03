namespace OnTheRoad.Domain.Models
{
    public interface ICity
    {
        string Name { get; }

        ICountry Country { get; }
    }
}
