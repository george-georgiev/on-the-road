using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Views;
using System;
using WebFormsMvp.Web;
using OnTheRoad.Mvp.EventArgsClasses;
using WebFormsMvp;
using OnTheRoad.Mvp.Presenters;

namespace OnTheRoad.Trips
{
    [PresenterBinding(typeof(AddTripPresenter))]
    public partial class AddTrip : MvpPage<TripModel>, IAddTripView
    {
        public event EventHandler<AddTripEventArgs> CreateTrip;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateTripButton_Click(object sender, EventArgs e)
        {
            //var categoryIds = this.Categories.SelecetedCategoryIds;
        }
    }
}