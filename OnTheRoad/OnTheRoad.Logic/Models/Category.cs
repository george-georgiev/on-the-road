using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Models
{
    public class Category : ICategory, IIdentifiable
    {
        public Category(string name)
        {
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
