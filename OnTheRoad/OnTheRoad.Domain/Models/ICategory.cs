namespace OnTheRoad.Domain.Models
{
    public interface ICategory : IIdentifiable
    {
        string Name { get; set; }
    }
}
