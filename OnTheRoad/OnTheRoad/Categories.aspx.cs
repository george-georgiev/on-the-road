using OnTheRoad.CustomControllers;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Views;
using System;
using WebFormsMvp;
using WebFormsMvp.Web;
using OnTheRoad.Mvp.EventArgsClasses;

namespace OnTheRoad
{
    [PresenterBinding(typeof(CategoriesPresenter))]
    public partial class Categories : MvpPage<CategoriesModel>, ICategoriesView
    {
        private const string CategoryNameParam = "categoryName";

        private const string CategoryOverviewUserControl = "~/CustomControllers/CategoryOverview.ascx";

        protected const int PageSize = 3;

        protected string CategoryName
        {
            get
            {
                return (string)this.RouteData.Values[CategoryNameParam];
            }
        }

        public event EventHandler GetCategories;

        public event EventHandler<CategoriesEventArgs> GetTrips;

        public event EventHandler<CategoriesEventArgs> GetTripsTotalCount;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this.CategoryName == null)
            {
                this.ShowCategoryOverviews();
            }
            else
            {
                this.ShowCategoryTrips(this.CategoryName);
            }
        }

        private void ShowCategoryOverviews()
        {
            this.GetCategories?.Invoke(this, new EventArgs());
            foreach (var category in this.Model.Categories)
            {
                var categoryOverview = (CategoryOverview)LoadControl(CategoryOverviewUserControl);
                categoryOverview.CategoryName = category.Name;
                this.PlaceHolderCategories.Controls.Add(categoryOverview);
            }

            this.DivCategories.Visible = true;
            this.DivCategoryTrips.Visible = false;
        }

        private void ShowCategoryTrips(string categoryName)
        {
            if (this.DataPager.Total == null)
            {
                this.GetTripsTotalCount(this, new CategoriesEventArgs() { CategoryName = categoryName });
                var total = this.Model.TripsTotalCount;
                this.DataPager.Total = total;
            }

            var skip = (this.DataPager.PageNumber - 1) * PageSize;
            this.LoadTrips(categoryName, skip, PageSize);

            this.DivCategories.Visible = false;
            this.DivCategoryTrips.Visible = true;
        }

        private void LoadTrips(string categoryName, int skip, int take)
        {
            this.GetTrips?.Invoke(this, new CategoriesEventArgs() { CategoryName = categoryName, Skip = skip, Take = take });
            this.ListViewTrips.DataSource = this.Model.Trips;
            this.ListViewTrips.DataBind();
        }
    }
}