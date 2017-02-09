using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Mvp.Models
{
    public class CategoriesModel
    {
        public IEnumerable<ICategory> Categories { get; set; }
    }
}
