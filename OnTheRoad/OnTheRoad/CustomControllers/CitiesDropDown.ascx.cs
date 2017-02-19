using System;
using OnTheRoad.Mvp.CustomControllers.Contracts;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Presenters;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace OnTheRoad.CustomControllers
{
    [PresenterBinding(typeof(CitiesPresenter))]
    public partial class CitiesDropDown : MvpUserControl<CitiesModel>, ICitiesView
    {
        public event EventHandler GetCities;

        public string SelectedCityName { get; set; }

        public string SelectCityName { get; set; }

        public int SelectedCityId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.GetCities?.Invoke(this, new EventArgs());
            this.DropDownCities.DataSource = this.Model.Cities;
            this.DropDownCities.DataBind();

            // TODO: .......
            //if (this.SelectCityName != null)
            //{
            //    this.Session.Add("city", this.SelectCityName);
            //}

            //this.DropDownCities.Items.FindByText(this.SelectCityName).Selected = true;
        }

        protected void DropDownCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedCityId = int.Parse(this.DropDownCities.SelectedValue);
        }
    }
}