using System;
using System.Collections.Generic;
using OnTheRoad.CustomControllers.Contracts;
using OnTheRoad.Models;
using OnTheRoad.Presenters;
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