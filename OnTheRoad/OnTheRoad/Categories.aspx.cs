using OnTheRoad.CustomControllers;
using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Views;
using System;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace OnTheRoad
{
    [PresenterBinding(typeof(CategoriesPresenter))]
    public partial class Categories : MvpPage<CategoriesModel>, ICategoriesView
    {
        private const string CategoryName = "categoryname";

        private const string CategoryOverviewUserControl = "~/CustomControllers/CategoryOverview.ascx";
        private const string TripsListUserControl = "~/CustomControllers/TripsList.ascx";

        private string CategoryNameQyeryParam
        {
            get
            {
                var categoryName = this.Request.QueryString[CategoryName];
                return categoryName;
            }
        }

        public event EventHandler GetCategories;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var categoryName = this.CategoryNameQyeryParam;

            if (categoryName == null)
            {
                this.ShowCategoryOverviews();
            }
            else
            {
                this.ShowCategoryTrips(categoryName);
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
            var tripsList = (TripsList)LoadControl(TripsListUserControl);
            tripsList.CategoryName = categoryName;
            tripsList.Label = $"Категория {categoryName}";
            this.PlaceHolderCategoryTrips.Controls.Add(tripsList);

            this.DivCategories.Visible = false;
            this.DivCategoryTrips.Visible = true;
        }
    }
}