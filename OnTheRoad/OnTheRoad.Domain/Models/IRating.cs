namespace OnTheRoad.Domain.Models
{
    public interface IRating : IIdentifiable
    {
        string Value { get; set; }
    }
}
