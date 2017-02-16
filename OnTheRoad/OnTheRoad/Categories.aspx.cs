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
        public event EventHandler GetCategories;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.GetCategories?.Invoke(this, new EventArgs());
            foreach (var category in this.Model.Categories)
            {
                var categoryOverview = (CategoryOverview)LoadControl("~/CustomControllers/CategoryOverview.ascx");
                categoryOverview.CategoryName = category.Name;
                this.PlaceHolderCategories.Controls.Add(categoryOverview);
            }
        }
    }
}