using OnTheRoad.Mvp.CustomControllers.Contracts;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Presenters;
using System;
using System.Collections.Generic;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace OnTheRoad.CustomControllers
{
    [PresenterBinding(typeof(CitiesPresenter))]
    public partial class CitiesDropDown : MvpUserControl<CitiesModel>, ICitiesView
    {
        public event EventHandler GetCities;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetCities?.Invoke(this, new EventArgs());
            this.DropDownCities.DataSource = new List<CitiesModel>() { this.Model };
            this.DropDownCities.DataBind();
        }
    }
}