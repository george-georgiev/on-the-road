using OnTheRoad.Logic.Contracts;
using OnTheRoad.Mvp.EventArgsClasses;
using OnTheRoad.Mvp.Views;
using System;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Presenters
{
    public class AddTripPresenter : Presenter<IAddTripView>
    {
        private  readonly IImageService imageService;
        private readonly ITripAddService tripAddService;
        private readonly ITripBuilder tripBuilder;

        public AddTripPresenter(IAddTripView view, ITripAddService tripAddService, ITripBuilder tripBuilder, IImageService imageService) : base(view)
        {
            if (tripAddService == null)
            {
                throw new ArgumentNullException("tripAddService can not be null!");
            }

            if (tripBuilder == null)
            {
                throw new ArgumentNullException("tripBuilder can not be null!");
            }

            if (imageService == null)
            {
                throw new ArgumentNullException("imageService can not be null!");
            }

            this.tripAddService = tripAddService;
            this.tripBuilder = tripBuilder;
            this.imageService = imageService;

            this.View.CreateTrip += View_CreateTrip;
            this.View.GetTripsDefaultImage += View_GetTripsDefaultImage;
        }

        private void View_GetTripsDefaultImage(object sender, EventArgs e)
        {
            var image = this.imageService.LoadResizedTripsImage();
            this.View.Model.ImageContent = image;
        }

        private void View_CreateTrip(object sender, AddTripEventArgs e)
        {
            var name = e.TripName;
            var description = e.Description;
            var location = e.Location;
            var startDate = e.StartDate;
            var endDate = e.EndDate;
            var coverImage = e.CoverImageContent;

            var trip = this.tripBuilder
                .SetName(name)
                .SetDescription(description)
                .SetLocation(location)
                .SetStartDate(startDate)
                .SetEndDate(endDate)
                .SetImage(coverImage)
                .Build();

            var loggedUsername = e.LoggedUserName;

            var categoryIds = e.SelectedCategoryIds;
            var tagNames = e.SelectedTagNames;

            this.tripAddService.AddTrip(trip, loggedUsername, categoryIds, tagNames);
        }
    }
}
