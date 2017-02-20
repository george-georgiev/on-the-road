using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Views;
using System;
using WebFormsMvp.Web;
using OnTheRoad.Mvp.EventArgsClasses;
using WebFormsMvp;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Domain.Enumerations;

namespace OnTheRoad.Trips
{
    [PresenterBinding(typeof(TripsPresenter))]
    public partial class Default : MvpPage<TripsModel>, ITripsView
    {
        protected const int PageSize = 3;
        private const string TripIdParam = "tripId";
        private const string SearchParam = "pattern";
        private const string TripsUrl = "/trips/";
        private const string TripsAllResultTitle = "Всички пътешествия";
        private const string TripsSearchResultTitle = "Резултати от търсенето за: ";

        public event EventHandler<GetTripEventArgs> GetTrip;
        public event EventHandler<GetTripsEventArgs> GetTrips;
        public event EventHandler<SearchTripsEventArgs> GetTripsBySearchPattern;
        public event EventHandler<SubscribeEventArgs> Subscribe;
        public event EventHandler<SearchTripsEventArgs> GetTripsSearchTotalCount;
        public event EventHandler<GetTripsEventArgs> GetTripsTotalCount;

        public string TripId
        {
            get
            {
                var tripId = (string)this.RouteData.Values[TripIdParam];

                return tripId;
            }
        }

        public string SearchPattern
        {
            get
            {
                var pattern = (string)this.RouteData.Values[SearchParam];

                return pattern;
            }
        }

        protected string TripsResultsTitle { get; set; }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this.TripId != null)
            {
                this.PrepareTripDetailsView();
            }
            else if (this.SearchPattern != null)
            {
                this.PrepairTripsSearchView();
            }
            else
            {
                this.PrepareTripsAllView();
            }
        }

        protected void DropDownListAttendance_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = this.DropDownListAttendance.SelectedValue;
            var subscriptionStatus = (SubscriptionStatus)Enum.Parse(typeof(SubscriptionStatus), selectedValue);
            var currentUserName = this.Context.User.Identity.Name;
            this.Subscribe?.Invoke(this, new SubscribeEventArgs() { CurrentUserName = currentUserName, TripId = int.Parse(this.TripId), SubscriptionStatus = subscriptionStatus });
        }

        private void PrepareTripsAllView()
        {
            if (this.DataPager.Total == null)
            {
                this.GetTripsTotalCount?.Invoke(this, new GetTripsEventArgs());
                var total = this.Model.TripsTotalCount;
                this.DataPager.Total = total;
            }

            var skip = (this.DataPager.PageNumber - 1) * PageSize;
            this.GetTrips?.Invoke(this, new GetTripsEventArgs() { Skip = skip, Take = PageSize });

            var title = TripsAllResultTitle;
            this.PrepareTripsResultComponents(title);
        }

        private void PrepairTripsSearchView()
        {
            if (this.DataPager.Total == null)
            {
                this.GetTripsSearchTotalCount?.Invoke(this, new SearchTripsEventArgs() { SearchPattern = this.SearchPattern });
                var total = this.Model.TripsTotalCount;
                this.DataPager.Total = total;
            }
            
            var skip = (this.DataPager.PageNumber - 1) * PageSize;
            this.GetTripsBySearchPattern?.Invoke(this, new SearchTripsEventArgs() { SearchPattern = this.SearchPattern, Skip = skip, Take = PageSize });

            var title = TripsSearchResultTitle + this.SearchPattern;
            this.PrepareTripsResultComponents(title);
        }

        private void PrepareTripsResultComponents(string title)
        {
            this.ListViewTrips.DataSource = this.Model.Trips;
            this.ListViewTrips.DataBind();

            this.TripsResultsTitle = title;

            this.PlaceHolderTrip.Visible = false;
            this.PlaceHolderTripsResult.Visible = true;
        }

        private void PrepareTripDetailsView()
        {
            var currentUserName = this.Context.User.Identity.Name;
            var tripId = this.ParseTripIdOrRedirect();
            this.GetTrip?.Invoke(this, new GetTripEventArgs() { TripId = tripId, CurrentUserName = currentUserName });

            this.HandleAttendanceDropDown(currentUserName);

            this.PlaceHolderTrip.Visible = true;
            this.PlaceHolderTripsResult.Visible = false;
        }

        private void HandleAttendanceDropDown(string currentUserName)
        {
            if (string.IsNullOrEmpty(currentUserName) || this.Model.IsOrganiser)
            {
                this.DropDownListAttendance.Visible = false;
            }
            else
            {
                var selectedValue = Enum.GetName(typeof(SubscriptionStatus), this.Model.SubscriptionStatus);
                this.DropDownListAttendance.SelectedValue = selectedValue;
                this.DropDownListAttendance.Visible = true;
            }
        }

        private int ParseTripIdOrRedirect()
        {
            int tripId;
            var isParsed = int.TryParse((string)this.RouteData.Values[TripIdParam], out tripId);
            if (!isParsed)
            {
                this.Response.Redirect(TripsUrl);
            }

            return tripId;
        }
    }
}