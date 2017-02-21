using System;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.CustomControllers.Contracts;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
{
    public class CitiesPresenter : Presenter<ICitiesView>
    {
        private readonly ICityService cityService;

        public CitiesPresenter(ICitiesView view, ICityService cityService) 
            : base(view)
        {
            if (cityService == null)
            {
                throw new ArgumentNullException("cityService cannot be null!");
            }

            this.cityService = cityService;
            this.View.GetCities += View_GetCities;
        }

        private void View_GetCities(object sender, EventArgs e)
        {
            var cities = this.cityService.GetAllCities();

            this.View.Model.Cities = cities;
        }
    }
}