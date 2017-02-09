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

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetCategories?.Invoke(this, new EventArgs());
            this.CategoryRepeater.DataSource = this.Model.Categories;
            this.CategoryRepeater.DataBind();
        }
    }
}