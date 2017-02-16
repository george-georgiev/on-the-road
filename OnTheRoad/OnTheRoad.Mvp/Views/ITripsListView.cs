using System;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Models;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Views
{
    public interface ITripsListView : IView<TripsModel>
    {
        event EventHandler<TripsListEventArgs> GetTrips;
    }
}