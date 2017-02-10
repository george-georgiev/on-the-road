using OnTheRoad.Mvp.Models;
using OnTheRoad.Mvp.Presenters;
using OnTheRoad.Mvp.Views;
using System;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace OnTheRoad.CustomControllers
{
    [PresenterBinding(typeof(CategoriesPresenter))]
    public partial class CategoriesDropDown : MvpUserControl<CategoriesModel>, ICategoriesView
    {
        public int SelectedCategoryId { get; set; }

        public event EventHandler GetCategories;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.GetCategories?.Invoke(this, new EventArgs());
            this.DropDownCategories.DataSource = this.Model.Categories;
            this.DropDownCategories.DataBind();
            this.SelectedCategoryId = int.Parse(this.DropDownCategories.SelectedValue);
        }

        protected void DropDownCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedCategoryId = int.Parse(this.DropDownCategories.SelectedValue);
        }
    }
}