namespace OnTheRoad.Domain.Models
{
    public interface IImage : IIdentifiable
    {
        string Path { get; set; }
    }
}
