using System;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Models;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Views
{
    public interface IHomeView : IView<HomeModel>
    {
        event EventHandler<GetHomePageInfoEventArgs> GetRecentTrips;

        event EventHandler<GetHomePageInfoEventArgs> GetAllTripsCount;

        event EventHandler<GetHomePageInfoEventArgs> GetAllUsersCount;
    }
}
