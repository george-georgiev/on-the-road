using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Models;
using System;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Views
{
    public interface IAddTripView : IView<TripModel>
    {
        event EventHandler<AddTripEventArgs> CreateTrip;

        event EventHandler GetTripsDefaultImage;
    }
}
