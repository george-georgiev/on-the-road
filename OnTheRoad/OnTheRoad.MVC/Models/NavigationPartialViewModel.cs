using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.MVC.Models
{
    public class NavigationPartialViewModel
    {
        public IEnumerable<ICategory> Categories { get; set; }
    }
}