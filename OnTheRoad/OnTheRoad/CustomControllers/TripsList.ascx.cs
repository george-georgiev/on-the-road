using System;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Views;
using WebFormsMvp.Web;
using OnTheRoad.Mvp.EventArgsClasses;
using WebFormsMvp;
using OnTheRoad.Mvp.Presenters;
using System.Collections.Generic;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.CustomControllers
{
    [PresenterBinding(typeof(TripsListPresenter))]
    public partial class TripsList : MvpUserControl<TripsModel>, ITripsListView
    {
        public string CategoryName { get; set; }

        public string Label { get; set; }

        public IEnumerable<ITrip> Trips { get; set; }

        public event EventHandler<TripsListEventArgs> GetTrips;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetTrips?.Invoke(this, new TripsListEventArgs() { CategoryName = this.CategoryName });
            this.ListViewTrips.DataSource = this.Model.Trips;
            this.ListViewTrips.DataBind();
        }
    }
}