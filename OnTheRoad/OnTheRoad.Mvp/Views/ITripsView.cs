using System;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Models;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Views
{
    public interface ITripsView : IView<TripsModel>
    {
        event EventHandler<GetTripsEventArgs> GetTrips;

        event EventHandler<GetTripsEventArgs> GetTripsTotalCount;

        event EventHandler<GetTripEventArgs> GetTrip;

        event EventHandler<SearchTripsEventArgs> GetTripsBySearchPattern;

        event EventHandler<SubscribeEventArgs> Subscribe;

        event EventHandler<SearchTripsEventArgs> GetTripsSearchTotalCount;
    }
}