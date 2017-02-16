using System;
using WebFormsMvp.Web;
using WebFormsMvp;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.CustomControllers.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;

namespace OnTheRoad.CustomControllers
{
    [PresenterBinding(typeof(CategoryOverviewPresenter))]
    public partial class CategoryOverview : MvpUserControl<TripsModel>, ICategoryOverviewView
    {
        public string CategoryName { get; set; }

        public event EventHandler<CategoryOverviewEventArgs> GetTrips;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.GetTrips?.Invoke(this, new CategoryOverviewEventArgs() { CategoryName = this.CategoryName });
            this.ListViewTrips.DataSource = this.Model.Trips;
            this.ListViewTrips.DataBind();
        }
    }
}