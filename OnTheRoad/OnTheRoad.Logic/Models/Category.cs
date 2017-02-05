using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Models
{
    public class Category : ICategory
    {
        public Category(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}
