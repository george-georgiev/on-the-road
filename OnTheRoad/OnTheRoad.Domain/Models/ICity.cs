namespace OnTheRoad.Domain.Models
{
    public interface ICity : IIdentifiable
    {
        string Name { get; set; }

        //ICountry Country { get; set; }
    }
}
