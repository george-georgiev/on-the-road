namespace OnTheRoad.Domain.Models
{
    public interface ITag : IIdentifiable
    {
        string Name { get; set; }
    }
}
