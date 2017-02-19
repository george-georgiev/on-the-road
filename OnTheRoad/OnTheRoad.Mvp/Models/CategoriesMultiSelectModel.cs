using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.Mvp.Models
{
    public class CategoriesMultiSelectModel
    {
        public IEnumerable<ICategory> Categories { get; set; }
    }
}
