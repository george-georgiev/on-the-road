using System;
using System.Web.UI;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Views;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace OnTheRoad
{
    [PresenterBinding(typeof(HomePresenter))]
    public partial class _Default : MvpPage<HomeModel>, IHomeView
    {
        public event EventHandler<GetHomePageInfoEventArgs> GetAllTripsCount;
        public event EventHandler<GetHomePageInfoEventArgs> GetAllUsersCount;
        public event EventHandler<GetHomePageInfoEventArgs> GetRecentTrips;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetAllTripsCount?.Invoke(this, new GetHomePageInfoEventArgs());
            this.GetAllUsersCount?.Invoke(this, new GetHomePageInfoEventArgs());
            this.GetRecentTrips?.Invoke(this, new GetHomePageInfoEventArgs());

            this.ListViewTrips.DataSource = this.Model.RecentTrips;
            this.Page.DataBind();
        }
    }
}