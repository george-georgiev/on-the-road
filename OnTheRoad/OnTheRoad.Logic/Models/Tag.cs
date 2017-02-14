using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Models
{
    public class Tag : ITag
    {
        public Tag(string name)
        {
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
