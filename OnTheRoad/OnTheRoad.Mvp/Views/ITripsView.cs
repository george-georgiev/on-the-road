using System;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Models;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Views
{
    public interface ITripsView : IView<TripsModel>
    {
        event EventHandler<TripsEventArgs> GetTrips;

        event EventHandler<TripsEventArgs> GetTrip;

        event EventHandler<TripsEventArgs> GetTripsBySearchPattern;

        event EventHandler<TripsEventArgs> Subscribe;
    }
}