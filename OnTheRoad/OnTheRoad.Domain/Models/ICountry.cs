namespace OnTheRoad.Domain.Models
{
    public interface ICountry : IIdentifiable
    {
        string Name { get; set; }
    }
}
