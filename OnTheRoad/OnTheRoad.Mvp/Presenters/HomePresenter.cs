using System;
using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Views;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
{
    public class HomePresenter : Presenter<IHomeView>
    {
        private const int RECENT_TRIPS_COUNT = 4;
        private readonly ITripGetService tripsService;
        private readonly IUserGetService usersService;

        public HomePresenter(IHomeView view, ITripGetService tripsService, IUserGetService usersService)
            : base(view)
        {
            if (tripsService == null)
            {
                throw new ArgumentNullException("tripsService Factory cannot be null");
            }

            if (usersService == null)
            {
                throw new ArgumentNullException("usersService Factory cannot be null");
            }

            this.tripsService = tripsService;
            this.usersService = usersService;
            this.View.GetRecentTrips += View_GetRecentTrips;
            this.View.GetAllTripsCount += View_GetAllTripsCount;
            this.View.GetAllUsersCount += View_GetAllUsersCount;
        }

        private void View_GetAllUsersCount(object sender, GetHomePageInfoEventArgs e)
        {
            var usersCount = this.usersService.GetAllUsersCount();
            this.View.Model.AllUsersCount = usersCount;
        }

        private void View_GetAllTripsCount(object sender, GetHomePageInfoEventArgs e)
        {
            var tripsCount = this.tripsService.GetTripsCount();
            this.View.Model.AllTripsCount = tripsCount;
        }

        private void View_GetRecentTrips(object sender, GetHomePageInfoEventArgs e)
        {
            var recentTrips = this.tripsService.GetTrips(0, RECENT_TRIPS_COUNT);
            this.View.Model.RecentTrips = recentTrips;
        }
    }
}
