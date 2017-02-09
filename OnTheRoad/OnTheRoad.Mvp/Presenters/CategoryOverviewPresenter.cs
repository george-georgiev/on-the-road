using OnTheRoad.Domain.Models;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Logic.Models;
using OnTheRoad.Mvp.CustomControllers.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using System;
using System.Collections.Generic;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
{
    public class CategoryOverviewPresenter : Presenter<ICategoryOverviewView>
    {
        private const int TripsCount = 4;
        private readonly ITripService tripService;

        public CategoryOverviewPresenter(ICategoryOverviewView view, ITripService tripService) : base(view)
        {
            if (tripService == null)
            {
                throw new ArgumentNullException("tripService can not be null!");
            }

            this.tripService = tripService;

            this.View.GetTrips += View_GetTrips; ;
        }

        private void View_GetTrips(object sender, CategoryOverviewEventArgs e)
        {
            //var trips = this.tripService.GetTripsOrderedByDateCreated(TripsCount);
            var trips = new List<ITrip>() { new Trip("Trip 1"), new Trip("Trip 1"), new Trip("Trip 1"), new Trip("Trip 1") };
            this.View.Model.Trips = trips;
        }
    }
}