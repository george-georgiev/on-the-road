using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Models;
using System;
using WebFormsMvp;

namespace OnTheRoad.Mvp.CustomControllers.Contracts
{
    public interface ICategoryOverviewView : IView<TripsModel>
    {
        event EventHandler<CategoryOverviewEventArgs> GetTrips;
    }
}
