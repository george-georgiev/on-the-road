using System.ComponentModel.DataAnnotations.Schema;

namespace OnTheRoad.Data.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
    }
}
