using System.Collections.Generic;

namespace OnTheRoad.MVC.Areas.User.Models
{
    public class CitiesAllViewModel
    {
        public IEnumerable<CityViewModel> Cities { get; set; }

        public string SelectedCityName { get; set; }
    }
}