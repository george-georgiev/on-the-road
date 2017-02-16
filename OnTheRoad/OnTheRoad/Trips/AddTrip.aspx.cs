using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Views;
using System;
using WebFormsMvp.Web;
using OnTheRoad.Mvp.EventArgsClasses;
using WebFormsMvp;
using OnTheRoad.Mvp.Presenters;
using System.Linq;
using System.IO;

namespace OnTheRoad.Trips
{
    [PresenterBinding(typeof(AddTripPresenter))]
    public partial class AddTrip : MvpPage<TripModel>, IAddTripView
    {
        private const char DateSeparator = '-';
        private const string ImageSessionKey = "Image";

        public event EventHandler<AddTripEventArgs> CreateTrip;

        public byte[] ImageContent
        {
            get
            {
                return (byte[])this.Session[ImageSessionKey];
            }
            set
            {
                this.Session[ImageSessionKey] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateTripButton_Click(object sender, EventArgs e)
        {
            var name = this.TripTitle.Text;
            var description = this.Description.Text;
            var location = this.Location.Text;
            var startDate = this.ConvertDate(this.StartDate.Text);
            var endDate = this.ConvertDate(this.EndDate.Text);
            var categoryIds = this.Categories.SelecetedCategoryIds;
            var tagNames = this.Tags.SelectedTagNames;

            var loggedUsername = this.Context.User.Identity.Name;

            var args = new AddTripEventArgs()
            {
                TripName = name,
                LoggedUserName = loggedUsername,
                Description = description,
                Location = location,
                StartDate = startDate,
                EndDate = endDate,
                SelectedCategoryIds = categoryIds,
                SelectedTagNames = tagNames,
                CoverImageContent = this.ImageContent
            };

            this.CreateTrip?.Invoke(this, args);
        }

        protected void ImageUploader_ImageUpload(object sender, ImageUploadEventArgs e)
        {
            this.LabelFileName.Text = e.FileName;
            this.ImageContent = e.ImageContent;
        }

        protected void ImageUploader_Error(object sender, ErrorEventArgs e)
        {
            var exception = e.GetException();
            var message = exception.Message;
            // TODO Implement toaster
            this.LabelImageErrors.Text = message;
        }

        private DateTime ConvertDate(string value)
        {
            var arr = value.Split(DateSeparator).Select(int.Parse).ToArray();
            var year = arr[0];
            var month = arr[1];
            var day = arr[2];

            var date = new DateTime(year, month, day);

            return date;
        }
    }
}